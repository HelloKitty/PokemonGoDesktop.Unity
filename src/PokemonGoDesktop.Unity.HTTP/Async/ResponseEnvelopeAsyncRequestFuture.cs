using Easyception;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// ResponseEnvelope-based implementation of <see cref="AsyncRequestFuture{TResponseMessageType}"/> to be used for async
	/// requests.
	/// </summary>
	/// <typeparam name="TResponseMessageType">Expected <see cref="IResponseMessage"/> type.</typeparam>
	public class ResponseEnvelopeAsyncRequestFuture<TResponseMessageType> : AsyncRequestFuture<TResponseMessageType>, IAsyncCallBackTarget
		where TResponseMessageType : class, IResponseMessage, IMessage, new()
	{
		/// <summary>
		/// Compiled lambda that will produce new instances of <see cref="TResponseMessageType"/>.
		/// </summary>
		private static Func<TResponseMessageType> compiledNewLambda { get; }

		static ResponseEnvelopeAsyncRequestFuture()
		{
			//new() in .Net 3.5 is VERY VERY slow with generic types
			//This is because Activator is very slow; we use compiled Lambda instead.
			compiledNewLambda = Expression.Lambda<Func<TResponseMessageType>>
											  (
											   Expression.New(typeof(TResponseMessageType))
											  ).Compile();
		}

		//We can't really know what thread this is on so we should lock
		private readonly object syncObj = new object();

		/// <summary>
		/// Indicates if the <see cref="IRestResponse"/> is available.
		/// </summary>
		public override bool isCompleted { get; protected set; }

		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response envelope async.
		/// </summary>
		/// <param name="envelope">Response envelope recieved.</param>
		public virtual void OnResponse(ResponseEnvelope envelope)
		{
			Throw<ArgumentNullException>.If.IsNull(envelope)?.Now(nameof(envelope), $"Recieved a null {nameof(ResponseEnvelope)}");

			//When this is called we should lock because we're about to dramatically change state
			lock (syncObj)
			{
				//We should check the bytes returned in a response
				//We expect a Payload.
				if (envelope.Returns.Count > 0)
				{
					TResponseMessageType responseProtoMessage = compiledNewLambda();

					//Take the bytes and push into the proto message
					responseProtoMessage.MergeFrom(envelope.Returns.First());

					if(responseProtoMessage != null)
					{
						ResultState = FutureState.Invalid;
						isCompleted = true;
					}
					else
					{
						ResultState = FutureState.Valid;
						Result = responseProtoMessage;
						isCompleted = true;
					}
				}
			}
		}
	}
}

using Easyception;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// <see cref="RestSharp"/> implementation of <see cref="AsyncRequestFuture{TResponseMessageType}"/> to be used for async
	/// requests.
	/// </summary>
	/// <typeparam name="TResponseMessageType"></typeparam>
	public sealed class RestSharpAsyncRequestFuture<TResponseMessageType> : AsyncRequestFuture<TResponseMessageType>, IRestSharpAsyncTarget
		where TResponseMessageType : class, IResponseMessage, IMessage, new()
	{
		/// <summary>
		/// Compiled lambda that will produce new instances of <see cref="TResponseMessageType"/>.
		/// </summary>
		private static Func<TResponseMessageType> compiledNewLambda { get; }

		static RestSharpAsyncRequestFuture()
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
		/// Internally managed response from <see cref="RestSharp"/>.
		/// </summary>
		private IRestResponse internalAsyncRecievedResponse { get; set; } = null;

		/// <summary>
		/// Indicates if the <see cref="IRestResponse"/> is available.
		/// </summary>
		public override bool isCompleted
		{
			get
			{
				lock(syncObj)
					return internalAsyncRecievedResponse != null;
			}
		}

		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response async.
		/// </summary>
		/// <param name="response">Response recieved.</param>
		/// <param name="handle">Async handle.</param>
		public void OnResponse(IRestResponse response, RestRequestAsyncHandle handle)
		{
			Throw<ArgumentNullException>.If.IsNull(response)?.Now(nameof(response), "Recieved a null response from RestSharp");

			//When this is called we should lock because we're about to dramatically change state
			lock (syncObj)
			{
				internalAsyncRecievedResponse = response;

				//We should check the bytes returned in a response
				//We expect a Payload.
				if (response.RawBytes != null && response.RawBytes.Count() > 0)
				{
					TResponseMessageType responseProtoMessage = compiledNewLambda();

					//Take the bytes and push into the proto message
					responseProtoMessage.MergeFrom(response.RawBytes);

					if(responseProtoMessage != null)
					{
						ResultState = ResponseState.Invalid;
					}
					else
					{
						ResultState = ResponseState.Valid;
						Result = responseProtoMessage;
					}
				}
			}
		}
	}
}

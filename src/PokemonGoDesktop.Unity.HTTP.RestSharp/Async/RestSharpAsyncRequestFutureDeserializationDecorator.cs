using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// <see cref="AsyncRequestFuture{TResponseMessageType}"/> Decorator that decorates the future with <see cref="ResponseEnvelope"/> deserialization.
	/// </summary>
	/// <typeparam name="TDecoratedFutureType">The future type to decorate.</typeparam>
	/// <typeparam name="TResponseMessageType">The response type the decorated future provides.</typeparam>
	public class RestSharpAsyncRequestFutureDeserializationDecorator<TDecoratedFutureType, TResponseMessageType> : AsyncRequestFuture<TResponseMessageType>, IAsyncRestResponseCallBackTarget
		where TResponseMessageType : class, IResponseMessage, IMessage<TResponseMessageType>, IMessage, new()
		where TDecoratedFutureType : IFuture<TResponseMessageType>, IAsyncCallBackTarget
	{
		/// <summary>
		/// Managed and decorated future.
		/// </summary>
		private TDecoratedFutureType decoratedFuture { get; }

		/// <summary>
		/// Indicates the state of the <see cref="Result"/>.
		/// </summary>
		public override FutureState ResultState
		{
			get
			{
				return decoratedFuture.ResultState;
			}

			protected set
			{
				//do nothing
			}
		}

		public override bool isCompleted
		{
			get
			{
				return decoratedFuture.isCompleted;
			}

			protected set
			{
				//do nothing
			}
		}

		public override TResponseMessageType Result
		{
			get
			{
				return decoratedFuture.Result;
			}

			protected set
			{
				//do nothing
			}
		}

		public RestSharpAsyncRequestFutureDeserializationDecorator(TDecoratedFutureType futureToDecorate)
		{
			decoratedFuture = futureToDecorate;
		}

		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response envelope async.
		/// </summary>
		/// <param name="envelope">Response envelope recieved.</param>
		public void OnResponse(IRestResponse response)
		{		
			ResponseEnvelope resEnv = new ResponseEnvelope();

			//At this point we've got a response
			//Probably a response envelope
			if (response.RawBytes != null && response.RawBytes.Length != 0)
			{
				resEnv.MergeFrom(response.RawBytes);
				decoratedFuture.OnResponse(resEnv);
			}

			//TODO: Handle exceptions
		}
	}

	/// <summary>
	/// <see cref="AsyncRequestFuture{TResponseMessageType}"/> Decorator that decorates the future with <see cref="ResponseEnvelope"/> deserialization.
	/// </summary>
	/// <typeparam name="TDecoratedFutureType">The future type to decorate.</typeparam>
	/// <typeparam name="TResponseMessageType">The response type the decorated future provides.</typeparam>
	public class RestSharpAsyncRequestFutureDeserializationDecorator<TDecoratedFutureType> : IFuture<ResponseEnvelope>, IAsyncRestResponseCallBackTarget
		where TDecoratedFutureType : IFuture<ResponseEnvelope>, IAsyncCallBackTarget
	{
		/// <summary>
		/// Managed and decorated future.
		/// </summary>
		private TDecoratedFutureType decoratedFuture { get; }

		/// <summary>
		/// Indicates the state of the <see cref="Result"/>.
		/// </summary>
		public FutureState ResultState
		{
			get
			{
				return decoratedFuture.ResultState;
			}

			protected set
			{
				//do nothing
			}
		}

		public bool isCompleted
		{
			get
			{
				return decoratedFuture.isCompleted;
			}

			protected set
			{
				//do nothing
			}
		}

		public ResponseEnvelope Result { get; private set; }

		public RestSharpAsyncRequestFutureDeserializationDecorator(TDecoratedFutureType futureToDecorate)
		{
			decoratedFuture = futureToDecorate;
		}

		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response envelope async.
		/// </summary>
		/// <param name="envelope">Response envelope recieved.</param>
		public void OnResponse(IRestResponse response)
		{
			//Debug.Log($"Recieved {this.GetType().Name} on response");

			ResponseEnvelope resEnv = new ResponseEnvelope();

			//At this point we've got a response
			//Probably a response envelope
			if (response.RawBytes != null && response.RawBytes.Length != 0)
			{
				resEnv.MergeFrom(response.RawBytes);
				decoratedFuture.OnResponse(resEnv);

				Result = resEnv;
			}
			else
				throw new InvalidOperationException($"No {nameof(ResponseEnvelope)} contained within message body of response. Code: {response.StatusCode}");

			//TODO: Handle exceptions
		}
	}
}

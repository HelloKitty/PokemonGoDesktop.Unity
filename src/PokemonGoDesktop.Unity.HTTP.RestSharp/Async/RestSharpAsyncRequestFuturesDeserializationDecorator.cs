using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	public class RestSharpAsyncRequestFuturesDeserializationDecorator<TDecoratedFutureType, TResponseMessageType> : AsyncRequestFutures<TResponseMessageType>, IAsyncRestResponseCallBackTarget
		where TResponseMessageType : class, IResponseMessage, IMessage<TResponseMessageType>, IMessage, new()
		where TDecoratedFutureType : IFuture<IEnumerable<TResponseMessageType>>, IAsyncCallBackTarget
	{
		private TDecoratedFutureType decoratedFuture { get; }

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

		public override IEnumerable<TResponseMessageType> Result
		{
			get
			{
				return decoratedFuture.Result;
			}

			protected set
			{
				base.Result = value;
			}
		}

		public RestSharpAsyncRequestFuturesDeserializationDecorator(TDecoratedFutureType futureToDecorate)
		{
			decoratedFuture = futureToDecorate;
		}

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
}

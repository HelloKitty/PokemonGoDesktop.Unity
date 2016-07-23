using PokemonGoDesktop.API.Proto.Services;
using PokemonGoDesktop.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Networking.Requests;
using PokemonGoDesktop.API.Proto;
using Google.Protobuf;

namespace PokemonGoDesktop.Unity.Common
{
	//TODO: Add pooling
	public class RequestBuilderService : IRequestBuilderService
	{
		public Request Build<TRequestMessageType>(TRequestMessageType messageInstance) 
			where TRequestMessageType : IRequestMessage, IMessage<TRequestMessageType>, IMessage
		{
			Request request = new Request();

			//push the bytes of the response payload into the request
			request.RequestMessage = messageInstance.ToByteString();

			return request;
		}

		public Request Build<TRequestMessageType>(RequestType requestType, TRequestMessageType messageInstance) 
			where TRequestMessageType : IRequestMessage, IMessage<TRequestMessageType>, IMessage
		{
			Request request = new Request();

			//push the bytes of the response payload into the request
			request.RequestMessage = messageInstance.ToByteString();

			return request;
		}
	}
}

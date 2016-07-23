using PokemonGoDesktop.API.Proto.Services;
using PokemonGoDesktop.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;

namespace PokemonGoDesktop.Unity.Common
{
	public class ResponseEnvelopeParserService : IResponseEnvelopeParserService
	{
		//compiled lamdas
		public IEnumerable<TResponseMessageType> ParseAll<TResponseMessageType>(ResponseEnvelope responseEnvelope) 
			where TResponseMessageType : IRequestMessage, IMessage<TResponseMessageType>, IMessage, new()
		{
			throw new NotImplementedException();
		}

		public TResponseMessageType ParseOne<TResponseMessageType>(ResponseEnvelope responseEnvelope) 
			where TResponseMessageType : IRequestMessage, IMessage<TResponseMessageType>, IMessage, new()
		{
			throw new NotImplementedException();
		}
	}
}

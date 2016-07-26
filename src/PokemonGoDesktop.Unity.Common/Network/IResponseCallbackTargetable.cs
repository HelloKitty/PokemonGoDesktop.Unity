using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Common
{
	/// <summary>
	/// Contract for being targetable for a network callback.
	/// </summary>
	public interface IResponseCallbackTargetable
	{
		/// <summary>
		/// Invoked as a callback for responses.
		/// </summary>
		/// <param name="response">Incoming response.</param>
		void OnResponse(IResponseMessage response);
	}

	/// <summary>
	/// Contract for being targetable for a network callback.
	/// </summary>
	public interface IResponseCallbackTargetable<TResponseMEssageType>
		where TResponseMEssageType : IResponseMessage, IMessage
	{
		/// <summary>
		/// Invoked as a callback for responses.
		/// </summary>
		/// <param name="response">Incoming response.</param>
		void OnResponse(TResponseMEssageType response);
	}
}

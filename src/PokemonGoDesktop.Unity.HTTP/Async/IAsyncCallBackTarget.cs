using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Implementer can recieve async response callbacks.
	/// </summary>
	public interface IAsyncCallBackTarget
	{
		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response envelope async.
		/// </summary>
		/// <param name="envelope">Response envelope recieved.</param>
		void OnResponse(ResponseEnvelope envelope);
	}
}

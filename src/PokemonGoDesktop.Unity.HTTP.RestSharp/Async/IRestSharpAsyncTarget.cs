using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// Implementer can recieve <see cref="RestSharp"/> async response callbacks.
	/// </summary>
	public interface IRestSharpAsyncTarget
	{
		/// <summary>
		/// Called when <see cref="RestSharp"/> recieves a response async.
		/// </summary>
		/// <param name="response">Response recieved.</param>
		/// <param name="handle">Async handle.</param>
		void OnResponse(IRestResponse response, RestRequestAsyncHandle handle);
	}
}

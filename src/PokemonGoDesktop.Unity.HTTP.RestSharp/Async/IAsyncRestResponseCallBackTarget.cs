using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// Contract for implementing async <see cref="IRestResponse"/> callback functionality.
	/// </summary>
	public interface IAsyncRestResponseCallBackTarget
	{
		/// <summary>
		/// Called on recieve of a <see cref="IRestResponse"/>
		/// </summary>
		/// <param name="response"></param>
		void OnResponse(IRestResponse response);
	}
}

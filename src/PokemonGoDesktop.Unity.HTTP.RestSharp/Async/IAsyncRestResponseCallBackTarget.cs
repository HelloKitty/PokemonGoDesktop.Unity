using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	public interface IAsyncRestResponseCallBackTarget
	{
		void OnResponse(IRestResponse response);
	}
}

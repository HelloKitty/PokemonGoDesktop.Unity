using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	[JsonObject]
	public interface ILoginSessionCookie
	{
		[JsonProperty(PropertyName = "lt")]
		string LT { get; }

		[JsonProperty(PropertyName = "execution")]
		string ExecutionID { get; }
	}
}

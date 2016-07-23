using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Represents the PTC Login Session Cookie Contract
	/// </summary>
	[JsonObject]
	public interface IPTCLoginSessionCookie
	{
		[JsonProperty(PropertyName = "lt")]
		string LT { get; }

		[JsonProperty(PropertyName = "execution")]
		string ExecutionID { get; }
	}
}

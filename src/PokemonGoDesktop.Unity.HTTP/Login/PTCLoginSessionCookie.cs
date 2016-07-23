using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	[JsonObject]
	public class PTCLoginSessionCookie : IPTCLoginSessionCookie
	{
		[JsonProperty(PropertyName = "lt", Order = 1)]
		public string LT { get; private set; }

		[JsonProperty(PropertyName = "execution", Order = 2)]
		public string ExecutionID { get; private set; }

		/// <summary>
		/// This property is only sent to us if we have errors in the initial session request.
		/// </summary>
		[JsonProperty(PropertyName = "errors", Required = Required.Default, Order = 3)]
		public List<string> ErrorStrings { get; private set; }

		/// <summary>
		/// Indicates if the session cookie was valid (Ex: Had no errors)
		/// </summary>
		public bool isValid
		{
			get { return ErrorStrings == null; }
		}
	}
}

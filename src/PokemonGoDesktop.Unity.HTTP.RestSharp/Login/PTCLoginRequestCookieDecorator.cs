using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	[JsonObject]
	public class PTCLoginRequestCookieDecorator : ILoginSessionCookie
	{
		[JsonProperty(PropertyName = "lt")]
		public string LT { get; private set; }

		[JsonProperty(PropertyName = "execution")]
		public string ExecutionID { get; private set; }

		[JsonProperty(PropertyName = "username")]
		public string UserName { get; private set; }

		//make private; couold help prevent stuff during demos
		[JsonProperty(PropertyName = "password")]
		private string Password { get; set; }

		[JsonProperty(PropertyName = "_eventId")]
		public string EventId { get;} = "submit";

		public PTCLoginRequestCookieDecorator(ILoginSessionCookie cookieData)
		{
			//set the cookie data
			LT = cookieData.LT;
			ExecutionID = cookieData.ExecutionID;
		}
	}

	//from Rocket-API
	/*new KeyValuePair<string, string>("lt", lt),
							new KeyValuePair<string, string>("execution", executionId),
							new KeyValuePair<string, string>("_eventId", "submit"),
							new KeyValuePair<string, string>("username", username),
							new KeyValuePair<string, string>("password", password),*/
}

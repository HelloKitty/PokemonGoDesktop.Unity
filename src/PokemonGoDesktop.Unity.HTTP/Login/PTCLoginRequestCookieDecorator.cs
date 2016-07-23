using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	[JsonObject]
	public class PTCLoginRequestCookieDecorator : IPTCLoginSessionCookie
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

		public PTCLoginRequestCookieDecorator(IPTCLoginSessionCookie cookieData)
		{
			//set the cookie data
			LT = cookieData.LT;
			ExecutionID = cookieData.ExecutionID;
		}
	}
}

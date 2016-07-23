using NUnit.Framework;
using PokemonGoDesktop.API.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp.Tests
{

	/*public const string RpcUrl = @"https://pgorelease.nianticlabs.com/plfe/rpc";
		public const string NumberedRpcUrl = @"https://pgorelease.nianticlabs.com/plfe/{0}/rpc";
		public const string PtcLoginUrl = "https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize";
		public const string PtcLoginOauth = "https://sso.pokemon.com/sso/oauth2.0/accessToken";
		public const string GoogleGrantRefreshAccessUrl = "https://android.clients.google.com/auth";*/

	[TestFixture]
	public class PTCRestSharpLoginTests
	{
		[Test]
		public static void Test()
		{
			PTCRestSharpLogin login = new PTCRestSharpLogin(@"https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize",
				@"https://sso.pokemon.com/sso/oauth2.0/accessToken", "[REDACTED]", "[REDACTED]");

			Assert.AreEqual(login.userLoginName, "[REDACTED]");
			Assert.AreEqual(login.loginRequestOAuthTokenUrl, @"https://sso.pokemon.com/sso/oauth2.0/accessToken");
			Assert.AreEqual(login.ptcLoginUrl, @"https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize");

			IAuthToken token = login.TryAuthenticate();

			Assert.IsTrue(token.isValid);
		}
	}
}

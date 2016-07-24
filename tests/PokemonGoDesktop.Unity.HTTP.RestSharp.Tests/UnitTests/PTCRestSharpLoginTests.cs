using NUnit.Framework;
using PokemonGoDesktop.API.Client.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp.Tests
{
	[TestFixture]
	public class PTCRestSharpLoginTests
	{
		[Test]
		public static void Test()
		{
			PTCRestSharpLogin login = new PTCRestSharpLogin(@"https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize",
				@"https://sso.pokemon.com/sso/oauth2.0/accessToken", "[redacted]", "[redacted]");

			Assert.AreEqual(login.loginRequestOAuthTokenUrl, @"https://sso.pokemon.com/sso/oauth2.0/accessToken");
			Assert.AreEqual(login.ptcLoginUrl, @"https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize");

			IAuthToken token = login.TryAuthenticate();

			Assert.IsTrue(token.isValid);
			Assert.IsTrue(!String.IsNullOrEmpty(token.TokenID));
		}
	}
}

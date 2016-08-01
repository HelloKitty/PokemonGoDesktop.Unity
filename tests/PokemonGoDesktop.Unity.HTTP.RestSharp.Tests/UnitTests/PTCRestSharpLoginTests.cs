using Google.Protobuf;
using NUnit.Framework;
using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

			RestSharpAsyncNetworkRequestService requestService = new RestSharpAsyncNetworkRequestService(@"https://pgorelease.nianticlabs.com");


			TestObject authTicketRes = new TestObject();

			RequestEnvelope envelope = new RequestEnvelope()
				.WithMessage(new Request() { RequestType = RequestType.GetPlayerProfile })
				.WithMessage(new Request() { RequestType = RequestType.GetHatchedEggs, RequestMessage = new GetPlayerMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.GetInventory, RequestMessage = new GetInventoryMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.CheckAwardedBadges, RequestMessage = new CheckAwardedBadgesMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.DownloadSettings, RequestMessage = new DownloadSettingsMessage() { Hash = "4a2e9bc330dae60e7b74fc85b98868ab4700802e" }.ToByteString() })
				.WithRequestID();

			envelope.WithAuthenticationMessage(API.Common.AuthType.PTC, token.TokenID);

			IFuture<ResponseEnvelope> response = requestService.SendRequestAsResponseFuture(envelope, authTicketRes, @"/plfe/rpc");

			Thread.Sleep(1000);

			Assert.NotNull(response);
			Assert.True(response.isCompleted);
			Assert.True(response.ResultState == FutureState.Valid);
			Assert.NotNull(authTicketRes.ApiUrl);

			RequestEnvelope env2 = new API.Proto.RequestEnvelope().WithRequestID();
			env2.WithRequestID();
			env2.WithAuthTicket(authTicketRes.ticket);
			env2.WithMessage(new Request() { RequestMessage = new GetPlayerMessage().ToByteString(), RequestType = RequestType.GetPlayer });

			IFuture<GetPlayerResponse> playerResponse = requestService.SendRequestAsFuture<GetPlayerResponse, ResponseEnvelopeAsyncRequestFuture<GetPlayerResponse>>(env2, new ResponseEnvelopeAsyncRequestFuture<GetPlayerResponse>(), $@"/plfe/{Regex.Match(authTicketRes.ApiUrl, @"([0-9]*)").Value}/rpc");

			Thread.Sleep(5000);

			Assert.NotNull(playerResponse);
			Assert.True(playerResponse.isCompleted);
			Assert.NotNull(playerResponse.Result);
			Assert.True(playerResponse.ResultState == FutureState.Valid);
		}

		public class TestObject : IFuture<ResponseEnvelope>, IAsyncCallBackTarget
		{
			public string ApiUrl;

			public AuthTicket ticket;

			public bool isCompleted { get; set; } = false;

			public ResponseEnvelope Result { get; set; }

			public FutureState ResultState { get; set; } = FutureState.Uncompleted;

			public void OnResponse(ResponseEnvelope envelope)
			{
				if (envelope == null)
					throw new ArgumentNullException(nameof(envelope));

				isCompleted = true;
				Result = envelope;
				ResultState = envelope != null ? FutureState.Valid : FutureState.Invalid;

				ApiUrl = envelope.ApiUrl;
				ticket = envelope.AuthTicket;
			}
		}
	}
}

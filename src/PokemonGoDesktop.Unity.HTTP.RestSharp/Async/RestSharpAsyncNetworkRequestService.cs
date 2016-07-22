using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Networking.Envelopes;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System.Net;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// <see cref="RestSharp"/> implementation of the <see cref="IAsyncNetworkRequestService"/> for sending requests.
	/// </summary>
	public class RestSharpAsyncNetworkRequestService : IAsyncNetworkRequestService
	{
		/// <summary>
		/// Managed <see cref="RestSharp"/> REST client.
		/// </summary>
		private RestClient httpClient { get; }

		/// <summary>
		/// Creates a new <see cref="RestSharpAsyncNetworkRequestService"/> with the default PokemonGo
		/// headers.
		/// </summary>
		public RestSharpAsyncNetworkRequestService()
		{
			//headers based on: https://github.com/FeroxRev/Pokemon-Go-Rocket-API/blob/master/PokemonGo.RocketAPI/Client.cs

			httpClient.AddDefaultHeader("User-Agent", "Niantic App");
			//"Dalvik/2.1.0 (Linux; U; Android 5.1.1; SM-G900F Build/LMY48G)");

			//Rocket-API has HttpClient continue expected setup so this is the equivalent.
			ServicePointManager.Expect100Continue = true;

			httpClient.AddDefaultHeader("Connection", "keep-alive");
			httpClient.AddDefaultHeader("Accept", "*/*");
			httpClient.AddDefaultHeader("Content-Type",
				"application/x-www-form-urlencoded");
		}

		//TODO: Add URL/URI functionality to request sending
		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <param name="onResponse">Optional delegate to invoke on response recieved.</param>
		/// <typeparam name="TResponseType">The response type expected back.</typeparam>
		/// <returns>An awaitable future result.</returns>
		public AsyncRequestFuture<TResponseType> SendRequest<TResponseType>(RequestEnvelope envolope, Action<TResponseType> onResponse = null)
			where TResponseType : class, IResponseMessage, IMessage, new()
		{
			RestSharpAsyncRequestFuture<TResponseType> future = new RestSharpAsyncRequestFuture<TResponseType>();

			//TODO: Add URL/URI
			IRestRequest request = new RestRequest().AddParameter(new Parameter() { Value = envolope.ToByteArray() });

			//To send protobuf requests 
			httpClient.PostAsync(request, future.OnResponse); //we have to provide the future as the callback

			return future;
		}
	}
}

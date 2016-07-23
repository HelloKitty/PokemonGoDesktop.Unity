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

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		public IFuture<ResponseEnvelope> SendRequest<TResponseType>(RequestEnvelope envolope, IFuture<TResponseType> responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		public IFuture<ResponseEnvelope> SendRequest<TResponseType>(RequestEnvelope envolope, IFuture<IEnumerable<TResponseType>> responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
		{
			throw new NotImplementedException();
		}
	}
}

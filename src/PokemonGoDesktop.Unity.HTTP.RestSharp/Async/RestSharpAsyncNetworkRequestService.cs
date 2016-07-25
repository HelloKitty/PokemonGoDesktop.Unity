using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using RestSharp;
using System.Net;
using Easyception;
using PokemonGoDesktop.Unity.HTTP.RestSharp;

namespace PokemonGoDesktop.Unity.HTTP.RestSharp
{
	/// <summary>
	/// <see cref="RestSharp"/> implementation of the <see cref="IAsyncNetworkRequestService"/> for sending requests.
	/// </summary>
	public class RestSharpAsyncNetworkRequestService : IAsyncNetworkRequestService, IResponseEnvelopeMiddlewareRegistry
	{
		/// <summary>
		/// Managed <see cref="RestSharp"/> REST client.
		/// </summary>
		private RestClient httpClient { get; }

		//TODO: Implement middlewares
		private Stack<IResponseEnvelopeMiddleware> responseEnvelopeMiddlewareStack { get; }

		/// <summary>
		/// Creates a new <see cref="RestSharpAsyncNetworkRequestService"/> with the default PokemonGo
		/// headers.
		/// </summary>
		public RestSharpAsyncNetworkRequestService()
		{
			//headers based on: https://github.com/FeroxRev/Pokemon-Go-Rocket-API/blob/master/PokemonGo.RocketAPI/Client.cs
			httpClient = new RestClient();


			httpClient.AddDefaultHeader("User-Agent", "Niantic App");
			//"Dalvik/2.1.0 (Linux; U; Android 5.1.1; SM-G900F Build/LMY48G)");

			//Rocket-API has HttpClient continue expected setup so this is the equivalent.
			ServicePointManager.Expect100Continue = true;

			httpClient.AddDefaultHeader("Connection", "keep-alive");
			httpClient.AddDefaultHeader("Accept", "*/*");
			httpClient.AddDefaultHeader("Content-Type",
				"application/x-www-form-urlencoded");

			//This is for an unused feature right now.
			//It will eventually be implemented
			responseEnvelopeMiddlewareStack = new Stack<IResponseEnvelopeMiddleware>();
		}


		public bool RegisterMiddleware(IResponseEnvelopeMiddleware middleware)
		{
			Throw<ArgumentNullException>.If.IsNull(nameof(middleware))?.Now(nameof(middleware), $"Cannot register a null {nameof(IResponseEnvelopeMiddleware)}");

			responseEnvelopeMiddlewareStack.Push(middleware);

			return true;
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		public IFuture<TResponseType> SendRequestAsFuture<TResponseType, TFutureType>(RequestEnvelope envelope, TFutureType responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
			where TFutureType : IFuture<TResponseType>, IAsyncCallBackTarget
		{
			//TODO: Add URL/URI
			//TODO: Verify header stuff
			IRestRequest request = new RestRequest().AddParameter(new Parameter() { Value = envelope.ToByteArray() });
			request.Method = Method.POST;

			var requestFuture = new RestSharpAsyncRequestFutureDeserializationDecorator<TFutureType, TResponseType>(responseMessageFuture);

			//To send protobuf requests 
			httpClient.ExecuteAsync(request, (res, hand) =>
			{
				requestFuture.OnResponse(res);
			}); //we have to provide the future as the callback

			return requestFuture;
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		public IFuture<IEnumerable<TResponseType>> SendRequestAsFutures<TResponseType, TFutureType>(RequestEnvelope envelope, TFutureType responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
			where TFutureType : IFuture<IEnumerable<TResponseType>>, IAsyncCallBackTarget
		{
			//TODO: Add URL/URI
			//TODO: Verify header stuff
			IRestRequest request = new RestRequest().AddParameter(new Parameter() { Value = envelope.ToByteArray() });
			request.Method = Method.POST;

			var requestFuture = new RestSharpAsyncRequestFuturesDeserializationDecorator<TFutureType, TResponseType>(responseMessageFuture);

			//To send protobuf requests 
			httpClient.ExecuteAsync(request, (res, hand) =>
			{
				requestFuture.OnResponse(res);
			}); //we have to provide the future as the callback

			return requestFuture;
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <see cref="IFuture{ResponseEnvelope}"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		public IFuture<ResponseEnvelope> SendRequestAsResponseFuture<TFutureType>(RequestEnvelope envelope, TFutureType responseEnvelopeFuture)
			where TFutureType : IFuture<ResponseEnvelope>, IAsyncCallBackTarget
		{
			//TODO: Add URL/URI
			//TODO: Verify header stuff
			IRestRequest request = new RestRequest().AddParameter(new Parameter() { Value = envelope.ToByteArray() });
			request.Method = Method.POST;

			var requestFuture = new RestSharpAsyncRequestFutureDeserializationDecorator<TFutureType>(responseEnvelopeFuture);

			httpClient.ExecuteAsync(request, (res, hand) =>
			{
				requestFuture.OnResponse(res);
			});

			return requestFuture;
		}
	}
}

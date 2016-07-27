using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonGoDesktop.API.Proto;
using UnityEngine;
using System.Collections;
using Easyception;
using Google.Protobuf;
using SceneJect.Common;
using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Proto.Services;
using System.Text.RegularExpressions;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Coroutine request/response <see cref="IAsyncUserNetworkRequestService"/> for async handling
	/// utilizing the Unity3D <see cref="MonoBehaviour"/> coroutines.
	/// </summary>
	[Injectee]
	public class NetworkCoroutineService : MonoBehaviour, IAsyncUserNetworkRequestService //must be MonoBehaviour to start the coroutines
	{
		/// <summary>
		/// Indicates how often the Coroutine service should poll the Futures for their completition status.
		/// </summary>
		[SerializeField]
		private float AsyncPollTimerSeconds;

		/// <summary>
		/// Internal network request service that user <see cref="RequestEnvelope"/>s are delegated to.
		/// </summary>
		[Inject]
		private readonly IAsyncNetworkRequestService innerAsyncNetworkService; //use readonly due to Sceneject

		/// <summary>
		/// Network session object.
		/// </summary>
		[Inject]
		private readonly IAuthableSession session;

		private string RpcURL = @"https://pgorelease.nianticlabs.com/plfe/rpc";

		/// <summary>
		/// Cached field for <see cref="AsyncPollTimerSeconds"/> yieldable.
		/// </summary>
		WaitForSeconds secondsWait;

		private void Awake()
		{
			Throw<InvalidOperationException>.If.IsTrue(AsyncPollTimerSeconds < 0)?.Now(); //TODO: Implement Easyception InvalidOperationException

			//create a cached wait
			secondsWait = new WaitForSeconds(AsyncPollTimerSeconds);
		}

		private void Start()
		{
			//verify state of the service
			//We'll pretend we're in a ctor and throw arg null
			Throw<ArgumentNullException>.If.IsNull(secondsWait)?.Now(nameof(secondsWait), $"Must have a non-null wait cache.");
			Throw<ArgumentNullException>.If.IsNull(innerAsyncNetworkService)?.Now(nameof(innerAsyncNetworkService), $"Must have a internal {nameof(IAsyncNetworkRequestService)}.");
		}

		private void PreProcessRequestEnvelope(RequestEnvelope envelope)
		{
			envelope.WithRequestID();

			if(!session.CurrentSessionState.HasFlag(SessionState.HasValidAuthTicket))
			{
				//If we don't have an auth ticket
				//Then we may need the request
				if (session.CurrentSessionState.HasFlag(SessionState.Authenticated))
				{
					envelope.WithAuthenticationMessage(session.Token.TokenType, session.Token.TokenID);
					return;
				}
			}
			else
			{
				envelope.WithAuthTicket(session.AuthenticationTicketContainer.Ticket);
				return;
			}

			throw new InvalidOperationException($"The session was not in a valid state for network requests.");
		}


		private string cachedApiString = null;
		private string GetRequestUrl()
		{
			if (!session.CurrentSessionState.HasFlag(SessionState.HasValidAuthTicket))
			{
				return @"/plfe/rpc";
			}
			else
			{
#if DEBUG || DEBUGBUILD
				string val = cachedApiString == null ? cachedApiString = $@"/plfe/{Regex.Match(session.AuthenticationTicketContainer.ApiUrl, @"([0-9]+)").Value}/rpc" : cachedApiString; //look here to see Regex in action https://regex101.com/r/eL3mK3/8

				Debug.Log($"RPC URL: {session.AuthenticationTicketContainer.ApiUrl} and Regex produces {Regex.Match(session.AuthenticationTicketContainer.ApiUrl, @"([0-9]+)").Value} and full string is {val}.");

				return val;
#else
				return cachedApiString == null ? cachedApiString = $@"/plfe/{Regex.Match(session.AuthenticationTicketContainer.ApiUrl, @"\/([0-9]+)").Value}/rpc" : cachedApiString; //look here to see Regex in action https://regex101.com/r/eL3mK3/8
#endif
			}

			throw new InvalidOperationException("The session was not in a valid state for network requests.");
		}

		//TODO: Document
		public AsyncRequestFuture<TResponseType> SendRequest<TResponseType>(RequestEnvelope envelope, IResponseCallbackTargetable<TResponseType> callback)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			return SendRequest(envelope, (Action<TResponseType>)(callback.OnResponse));
		}

		public AsyncRequestFutures<TResponseType> SendRequest<TResponseType>(RequestEnvelope envelope, IResponsesCallbackTargetable<TResponseType> callback)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			return SendRequest(envelope, (Action<IEnumerable<TResponseType>>)(callback.OnResponse));
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="IEnumerable{TResponseType}"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <param name="onResponse">Optional delegate to invoke on response recieved.</param>
		/// <typeparam name="TResponseType">The response type expected back.</typeparam>
		/// <returns>An awaitable future result.</returns>
		public AsyncRequestFutures<TResponseType> SendRequest<TResponseType>(RequestEnvelope envelope, Action<IEnumerable<TResponseType>> onResponse)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			//the delegate can be null. that is fine.
			Throw<ArgumentNullException>.If.IsNull(envelope)?.Now(nameof(envelope), $"Cannot send a null {nameof(RequestEnvelope)} with {nameof(NetworkCoroutineService)}");

			PreProcessRequestEnvelope(envelope);

			ResponseEnvelopeAsyncRequestFutures<TResponseType> responseFutures = new ResponseEnvelopeAsyncRequestFutures<TResponseType>();

			throw new NotImplementedException("Multiple response handling isn't implemented yet. API supports but no implementation.");
		}

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <param name="onResponse">Optional delegate to invoke on response recieved.</param>
		/// <typeparam name="TResponseType">The response type expected back.</typeparam>
		/// <returns>An awaitable future result.</returns>
		public AsyncRequestFuture<TResponseType> SendRequest<TResponseType>(RequestEnvelope envelope, Action<TResponseType> onResponse)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			//the delegate can be null. that is fine.
			Throw<ArgumentNullException>.If.IsNull(envelope)?.Now(nameof(envelope), $"Cannot send a null {nameof(RequestEnvelope)} with {nameof(NetworkCoroutineService)}");

			PreProcessRequestEnvelope(envelope);

			//We need to generate the async token and pass it to the coroutine
			//so that we can provide it to the caller and pass it to the internal service
			ResponseEnvelopeAsyncRequestFuture<TResponseType> responseFuture = new ResponseEnvelopeAsyncRequestFuture<TResponseType>();

			//Start the coroutine to service the request async
			StartCoroutine(RequestHandlingCoroutine(envelope, responseFuture, onResponse));

			//Provide the user with the token (but let's be serious, they're noobs and won't even look at it)
			return responseFuture;
		}

		public IFuture<ResponseEnvelope> SendRequest(RequestEnvelope envelope, Action<ResponseEnvelope> onResponse = null)
		{
			//the delegate can be null. that is fine.
			Throw<ArgumentNullException>.If.IsNull(envelope)?.Now(nameof(envelope), $"Cannot send a null {nameof(RequestEnvelope)} with {nameof(NetworkCoroutineService)}");

			PreProcessRequestEnvelope(envelope);

			ResponseEnvelopeAsyncRequestFuture responseFuture = new ResponseEnvelopeAsyncRequestFuture();

			StartCoroutine(RequestHandlingCoroutine(envelope, responseFuture, onResponse));

			return responseFuture;
		}

		/// <summary>
		/// Handles a request using a Unity3D coroutine. Yields waits until the response has been recieved.
		/// </summary>
		/// <typeparam name="TResponseType">The expected response type.</typeparam>
		/// <param name="envelope">Request envelope to send.</param>
		/// <param name="userFuture">User provided future (the future the user is holding)</param>
		/// <param name="onResponseCallback">The callback requested.</param>
		/// <returns>An enumerable-style collection. (Coroutine)</returns>
		private IEnumerator RequestHandlingCoroutine(RequestEnvelope envelope, ResponseEnvelopeAsyncRequestFuture userFuture, Action<ResponseEnvelope> onResponseCallback)
		{
			//Pass the request to the internal network service and poll the future when it's done
			IFuture<ResponseEnvelope> internalProvidedFuture = innerAsyncNetworkService.SendRequestAsResponseFuture(envelope, userFuture, GetRequestUrl());

			//generic polling for futures and callbacks
			yield return WaitForCompletedRequest(internalProvidedFuture, onResponseCallback);
		}

		//TODO: Consolidate all async request/response in a single coroutine.
		/// <summary>
		/// Handles a request using a Unity3D coroutine. Yields waits until the response has been recieved.
		/// </summary>
		/// <typeparam name="TResponseType">The expected response type.</typeparam>
		/// <param name="envelope">Request envelope to send.</param>
		/// <param name="userFuture">User provided future (the future the user is holding)</param>
		/// <param name="onResponseCallback">The callback requested.</param>
		/// <returns>An enumerable-style collection. (Coroutine)</returns>
		private IEnumerator RequestHandlingCoroutine<TResponseType>(RequestEnvelope envelope, ResponseEnvelopeAsyncRequestFuture<TResponseType> userFuture, Action<TResponseType> onResponseCallback)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			//Pass the request to the internal network service and poll the future when it's done
			IFuture<TResponseType> internalProvidedFuture = innerAsyncNetworkService.SendRequestAsFuture<TResponseType, ResponseEnvelopeAsyncRequestFuture<TResponseType>>(envelope, userFuture, GetRequestUrl());

			//generic polling for futures and callbacks
			yield return WaitForCompletedRequest(internalProvidedFuture, onResponseCallback);
		}

		/// <summary>
		/// Generic wait for future to complete with optional callback.
		/// </summary>
		/// <typeparam name="TFutureType">Future type to wait on.</typeparam>
		/// <param name="internalProvidedFuture">Internally provided future to wait on.</param>
		/// <param name="onResultCallback">Optional callback.</param>
		/// <returns></returns>
		private IEnumerator WaitForCompletedRequest<TFutureType>(IFuture<TFutureType> internalProvidedFuture, Action<TFutureType> onResultCallback = null)
			where TFutureType : class
		{
			while (!internalProvidedFuture.isCompleted)
			{
#if DEBUG || DEBUGBUILD
				Debug.Log("Waiting for response.");
#endif
				yield return secondsWait;
			}

			//Once we hit here the request has been completed.
			//Could technically be failed but the API doesn't expose that to this higher level service.
			//WARNING: There is a slight-ish race condition were we could reach this section of code after the user queried the Token we provided him.

			if (internalProvidedFuture.Result == null)
				throw new InvalidOperationException($"Async network service produced null {nameof(TFutureType)} in {nameof(NetworkCoroutineService)}");
			else
			{
				//we should push the result to the user callback if it's not null.
				//Users may not actually want a callback and just wanted the request sent
				if (onResultCallback != null) //don't listen to VS. This can be null
					onResultCallback(internalProvidedFuture.Result);
			}
		}

		//TODO: Consolidate all async request/response in a single coroutine.
		/// <summary>
		/// Handles a request using a Unity3D coroutine. Yields waits until the responses has been recieved.
		/// </summary>
		/// <typeparam name="TResponseType">The expected response type.</typeparam>
		/// <param name="envelope">Request envelope to send.</param>
		/// <param name="userFuture">User provided future (the future the user is holding)</param>
		/// <param name="onResponseCallback">The callback requested.</param>
		/// <returns>An enumerable-style collection. (Coroutine)</returns>
		private IEnumerator RequestHandlingCoroutine<TResponseType>(RequestEnvelope envelope, ResponseEnvelopeAsyncRequestFutures<TResponseType> userFutures, Action<IEnumerable<TResponseType>> onResponseCallback)
			where TResponseType : class, IResponseMessage, IMessage, IMessage<TResponseType>, new()
		{
			//Pass the request to the internal network service and poll the future when it's done
			IFuture<IEnumerable<TResponseType>> internalProvidedFuture = innerAsyncNetworkService.SendRequestAsFutures<TResponseType, ResponseEnvelopeAsyncRequestFutures<TResponseType>>(envelope, userFutures, GetRequestUrl());

			//generic polling for futures and callbacks
			yield return WaitForCompletedRequest(internalProvidedFuture, onResponseCallback);
		}
	}
}

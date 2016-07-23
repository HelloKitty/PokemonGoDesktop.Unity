using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// User facing network <see cref="RequestEnvelope"/> request sending service.
	/// </summary>
	//TODO: Add URL/URI functionality to request sending
	public interface IAsyncUserNetworkRequestService
	{
		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <param name="onResponse">Optional delegate to invoke on response recieved.</param>
		/// <typeparam name="TResponseType">The response type expected back.</typeparam>
		/// <returns>An awaitable future result.</returns>
		AsyncRequestFuture<TResponseType> SendRequest<TResponseType>(RequestEnvelope envolope, Action<TResponseType> onResponse = null)
			where TResponseType : class, IResponseMessage, IMessage, new();

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="IEnumerable{TResponseType}"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <param name="onResponse">Optional delegate to invoke on response recieved.</param>
		/// <typeparam name="TResponseType">The response type expected back.</typeparam>
		/// <returns>An awaitable future result.</returns>
		AsyncRequestFutures<TResponseType> SendRequest<TResponseType>(RequestEnvelope envolope, Action<IEnumerable<TResponseType>> onResponse = null)
			where TResponseType : class, IResponseMessage, IMessage, new();
	}
}

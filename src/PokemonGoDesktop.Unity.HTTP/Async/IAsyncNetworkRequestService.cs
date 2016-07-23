using Google.Protobuf;
using Networking.Envelopes;
using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Network <see cref="RequestEnvelope"/> request sending service.
	/// </summary>
	//TODO: Add URL/URI functionality to request sending
	public interface IAsyncNetworkRequestService
	{
		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		IFuture<ResponseEnvelope> SendRequest<TResponseType>(RequestEnvelope envolope, IFuture<TResponseType> responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new();

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envolope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		IFuture<ResponseEnvelope> SendRequest<TResponseType>(RequestEnvelope envolope, IFuture<IEnumerable<TResponseType>> responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new();
	}
}

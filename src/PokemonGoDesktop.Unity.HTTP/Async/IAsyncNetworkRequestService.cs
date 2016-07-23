using Google.Protobuf;
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
		/// <param name="envelope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		IFuture<TResponseType> SendRequestAsFuture<TResponseType, TFutureType>(RequestEnvelope envelope, TFutureType responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
			where TFutureType : AsyncRequestFuture<TResponseType>, IAsyncCallBackTarget;

		/// <summary>
		/// Tries to send the <see cref="RequestEnvelope"/> message to the network.
		/// Returns an <typeparamref name="TResponseType"/> when completed.
		/// </summary>
		/// <param name="envelope">Envolope to send.</param>
		/// <returns>An awaitable future result.</returns>
		IFuture<IEnumerable<TResponseType>> SendRequestAsFutures<TResponseType, TFutureType>(RequestEnvelope envelope, TFutureType responseMessageFuture)
			where TResponseType : class, IResponseMessage, IMessage<TResponseType>, IMessage, new()
			where TFutureType : AsyncRequestFutures<TResponseType>, IAsyncCallBackTarget;
	}
}

using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// An awaitable <see cref="IFuture{TFutureType}"/> implementation that contains
	/// a <typeparamref name="TResponseMessage"/> type result.
	/// </summary>
	/// <typeparam name="TResponseMessageType"></typeparam>
	public abstract class AsyncRequestFuture<TResponseMessageType> : IFuture<TResponseMessageType>
		where TResponseMessageType : class, IResponseMessage, IMessage, new()
	{
		/// <summary>
		/// Indicates the state of the <see cref="Result"/>.
		/// </summary>
		public virtual FutureState ResultState { get; protected set; } = FutureState.Uncompleted;

		/// <summary>
		/// Indicates if the request has completed.
		/// </summary>
		public virtual bool isCompleted { get; protected set; } = false;

		/// <summary>
		/// The Response result when completed. Will be null if incompleted.
		/// </summary>
		public virtual TResponseMessageType Result { get; protected set; } = null;
	}
}

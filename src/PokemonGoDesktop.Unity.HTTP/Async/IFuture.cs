using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Represents a future (like a task) of the specified
	/// value and type <paramref name="TFutureType"/>.
	/// </summary>
	/// <typeparam name="TFutureType">The resulting future type.</typeparam>
	public interface IFuture<out TFutureType>
		where TFutureType : class
	{
		/// <summary>
		/// Indicates if the result is available.
		/// </summary>
		bool isCompleted { get; }

		/// <summary>
		/// Represents the future response.
		/// Usually null if unavailable.
		/// </summary>
		TFutureType Result { get; }

		/// <summary>
		/// Indicates the state of the <see cref="Result"/>.
		/// </summary>
		FutureState ResultState { get; }
	}
}

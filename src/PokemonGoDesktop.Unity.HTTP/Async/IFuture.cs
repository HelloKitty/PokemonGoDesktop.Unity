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
	public interface IFuture<TFutureType>
		where TFutureType : class
	{
		/// <summary>
		/// Indicates if the result is available.
		/// </summary>
		bool isCompleted { get; }

		TFutureType Result { get; }
	}
}

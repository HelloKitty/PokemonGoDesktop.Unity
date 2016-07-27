using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Common
{
	//based on net40 IObserver<T> https://msdn.microsoft.com/en-us/library/dd783449(v=vs.110).aspx
	/// <summary>
	/// Class contract for being targted by <see cref="IObservable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Type to observe.</typeparam>
	public interface IObserver<in T>
	{
		/// <summary>
		/// Called when the <see cref="IObservable{T}"/> publishes
		/// a value.
		/// </summary>
		/// <param name="value">Value that was published.</param>
		void OnObserved(T value);
	}
}

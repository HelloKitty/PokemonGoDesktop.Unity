using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Common
{
	//based on net40 IObseravble<T> https://msdn.microsoft.com/en-us/library/dd990377(v=vs.110).aspx
	public interface IObservable<out T>
	{
		/// <summary>
		/// Subscribers an observer for values of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="observer">Observer to recieve values.</param>
		void Subscribe(IObserver<T> observer);
	}
}

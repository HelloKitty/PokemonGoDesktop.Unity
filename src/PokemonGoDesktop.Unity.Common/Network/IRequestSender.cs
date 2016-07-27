using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Common
{
	/// <summary>
	/// Contract for objects that can send requests without any input required. 
	/// </summary>
	public interface IRequestSender
	{
		/// <summary>
		/// Sends the internally managed request.
		/// </summary>
		void SendRequest();
	}
}

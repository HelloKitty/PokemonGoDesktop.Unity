using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity
{
	/// <summary>
	/// Indicates a loggable and possibly handable login server exception.
	/// </summary>
	public class ServerLoginException : Exception
	{
		public ServerLoginException(string message) 
			: base(message)
		{

		}

		public ServerLoginException(string message, Exception innerException) 
			: base(message, innerException)
		{

		}
	}
}

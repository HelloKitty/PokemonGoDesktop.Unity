using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Indicates the state of the response.
	/// </summary>
	public enum ResponseState
	{
		/// <summary>
		/// Indicates an uncompleted async request.
		/// </summary>
		Uncompleted,

		/// <summary>
		/// Indicates an invalid response.
		/// </summary>
		Invalid,

		/// <summary>
		/// Indicates a valid response.
		/// </summary>
		Valid
	}
}

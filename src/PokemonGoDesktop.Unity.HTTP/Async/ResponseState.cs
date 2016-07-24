using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Indicates the state of the future.
	/// </summary>
	public enum FutureState
	{
		/// <summary>
		/// Indicates an uncompleted async future.
		/// </summary>
		Uncompleted,

		/// <summary>
		/// Indicates an invalid future.
		/// </summary>
		Invalid,

		/// <summary>
		/// Indicates a valid future.
		/// </summary>
		Valid
	}
}

using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Common;
using PokemonGoDesktop.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Common
{
	/// <summary>
	/// Service that produces <see cref="IUserAuthenticationService"/> instances.
	/// </summary>
	public interface IUserAuthServiceFactory
	{
		/// <summary>
		/// Creates a new <see cref="IUserAuthenticationService"/> with the provided <see cref="AuthType"/>.
		/// </summary>
		/// <param name="authType">Authentication type.</param>
		/// <returns>A non-null instance of an <see cref="IUserAuthenticationService"/>.</returns>
		IUserAuthenticationService Create(AuthType authType, AuthenticationDetails details);
	}
}

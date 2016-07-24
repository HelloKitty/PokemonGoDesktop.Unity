using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Common;
using PokemonGoDesktop.Unity.Common;
using PokemonGoDesktop.Unity.HTTP.RestSharp;
using UnityEngine;
using Easyception;

namespace PokemonGoDesktop.Unity.Client
{
	public class UserAuthServiceFactory : IUserAuthServiceFactory
	{

		private IDictionary<AuthType, LoginUrlDetails> detailsAuthTypeMap { get; }

		public UserAuthServiceFactory(IDictionary<AuthType, LoginUrlDetails> detailsMaps)
		{
			Throw<ArgumentNullException>.If.IsNull(detailsMaps)?.Now(nameof(detailsMaps), "Auth type url details map must be valid.");

			detailsAuthTypeMap = detailsMaps;
		}

		/// <summary>
		/// Creates a new <see cref="IUserAuthenticationService"/> with the provided <see cref="AuthType"/>.
		/// </summary>
		/// <param name="authType">Authentication type.</param>
		/// <returns>A non-null instance of an <see cref="IUserAuthenticationService"/>.</returns>
		public IUserAuthenticationService Create(AuthType authType, AuthenticationDetails details)
		{
					
			switch (authType)
			{
				case AuthType.Google:
					throw new NotImplementedException("Google login support is not yet implemented.");
				case AuthType.PTC:
					try
					{
						return new PTCRestSharpLogin(detailsAuthTypeMap[authType].LoginUrl, detailsAuthTypeMap[authType].OAuthUrl, details.LoginString, details.Password);
					}
					catch (Exception e)
					{
						throw new InvalidOperationException("Failed to properly construct a PTC login service.", e);
					}
				default:
					Throw<ArgumentException>.If.Now($"Provide {nameof(AuthType)} was invalid.", nameof(authType));
					return null; //above throws
			}
		}
	}
}

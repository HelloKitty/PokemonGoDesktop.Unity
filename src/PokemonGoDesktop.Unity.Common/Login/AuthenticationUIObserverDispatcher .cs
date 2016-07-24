using Easyception;
using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.Common
{
	[Injectee]
	public class AuthenticationUIObserverDispatcher : MonoBehaviour
	{
		/// <summary>
		/// Indicates the authentication type for this dispatcher.
		/// </summary>
		[SerializeField]
		private AuthType AuthenticationType;

		/// <summary>
		/// String required for a login/authentication
		/// (Ex. Username, Email, One-off token)
		/// </summary>
		public string LoginString { get; private set; }

		/// <summary>
		/// Password used for authentication
		/// </summary>
		public string Password { get; private set; }

		[Serializable]
		public class AuthTokenEvent : UnityEvent<IAuthToken> { }

		[SerializeField]
		private AuthTokenEvent OnValidAuthToken;

		[SerializeField]
		private AuthTokenEvent OnInvalidAuthToken;

		[Inject]
		private IUserAuthServiceFactory authServiceFactory { get; set; }

		private void Awake()
		{
			Throw<ArgumentNullException>.If.IsNull(authServiceFactory)?.Now(nameof(authServiceFactory), $"{nameof(IUserAuthServiceFactory)} cannot be null.");
		}

		//These setters are here for hooking to UnityEvents
		/// <summary>
		/// Sets the <see cref="LoginString"/> value.
		/// Used to hook to <see cref="UnityEvent"/>
		/// </summary>
		/// <param name="value">Login string value.</param>
		public void SetLoginString(string value)
		{
			LoginString = value;
		}

		/// <summary>
		/// Sets the <see cref="Password"/> value.
		/// Used to hook to <see cref="UnityEvent"/>
		/// </summary>
		/// <param name="value">Password value.</param>
		public void SetPasswordString(string value)
		{
			Password = value;
		}

		public void TryAuthenticate()
		{
			Throw<ArgumentNullException>.If.IsNull(LoginString)?.Now(nameof(LoginString), $"{nameof(LoginString)} cannot be null.");
			Throw<ArgumentNullException>.If.IsNull(Password)?.Now(nameof(Password), $"{nameof(Password)} cannot be null.");

			IUserAuthenticationService service = authServiceFactory.Create(AuthenticationType, new AuthenticationDetails(LoginString, Password));

			Throw<ArgumentNullException>.If.IsNull(service)?.Now(nameof(service), $"{authServiceFactory} produced a null auth service.");

			IAuthToken token = service.TryAuthenticate();

			if (token == null || !token.isValid)
				OnInvalidAuthToken?.Invoke(token);

			OnValidAuthToken?.Invoke(token);
		}
	}
}

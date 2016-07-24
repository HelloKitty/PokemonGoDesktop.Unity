using Easyception;
using PokemonGoDesktop.API.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.Common
{
	public class UnityPokemonGoClientSession : MonoBehaviour, IAuthableSession
	{
		/// <summary>
		/// Indicates the current <see cref="SessionState"/>.
		/// </summary>
		public SessionState CurrentSessionState { get; private set; }

		/// <summary>
		/// Current <see cref="IAuthToken"/> associated with the session.
		/// </summary>
		public IAuthToken Token { get; private set; }

		/// <summary>
		/// Invoked when the <see cref="CurrentSessionState"/> changes.
		/// </summary>
		public event OnSessionStateChanged StatusChangedEvent;

		/// <summary>
		/// Sets the <see cref="IAuthableSession"/>s <see cref="IAuthToken"/> field <see cref="Token"/>
		/// if the token is valid.
		/// </summary>
		/// <param name="token">A valid authentication token.</param>
		/// <exception cref="ArgumentNullException">If the token is null.</exception>
		/// <exception cref="InvalidOperationException">If the token is in an invalid state.</exception>
		public void SetAuthenticationToken(IAuthToken token)
		{
#if DEBUG || DEBUGBUILD
			Debug.Log($"Successfully authenticated and recieved token with ID: {token.TokenID}");
#endif
			Throw<ArgumentNullException>.If.IsNull(token)?.Now(nameof(token), $"Recieved a null {nameof(IAuthToken)} during auth token set.");

			if (!token.isValid)
				Throw<InvalidOperationException>.If.Now(); //TODO: Add Easyception exception message

			Token = token;

			//There was no reason to reauth
			if (CurrentSessionState.HasFlag(SessionState.Authenticated))
				return;

			CurrentSessionState = CurrentSessionState | SessionState.Authenticated;

			//Call the event handler if it's not null
			StatusChangedEvent?.Invoke(CurrentSessionState);		
		}
	}
}

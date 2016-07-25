using Easyception;
using PokemonGoDesktop.API.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using PokemonGoDesktop.API.Proto;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.Common
{
	/// <summary>
	/// Unity3D implementation of the <see cref="IAuthableSession"/>.
	/// </summary>
	public class UnityPokemonGoClientSession : MonoBehaviour, IAuthableSession
	{
		public AuthTicket AuthTicket { get; private set; }

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

		[SerializeField]
		private OnSessionChangedEvent OnStatusChanged;

		void Awake()
		{
			//rig up the event to the Unity event
			StatusChangedEvent += (st) => OnStatusChanged?.Invoke(st);
		}

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
			Debug.Log($"Successfully authenticated and recieved valid oAuth.");
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

		/// <summary>
		/// Sets the <see cref="IAuthableSession"/>s <see cref="IAuthToken"/> field <see cref="Token"/>
		/// if the token is valid.
		/// </summary>
		/// <param name="token">A valid authentication token.</param>
		/// <exception cref="ArgumentNullException">If the token is null.</exception>
		/// <exception cref="InvalidOperationException">If the token is in an invalid state.</exception>
		public void SetAuthenticationTicket(AuthTicket ticket)
		{
#if DEBUG || DEBUGBUILD
			Debug.Log($"Successfully authenticated and recieved valid oAuth.");
#endif
			Throw<ArgumentNullException>.If.IsNull(ticket)?.Now(nameof(ticket), $"Recieved a null {nameof(AuthTicket)} during auth token set.");

			AuthTicket = ticket;

			//There was no reason to reauth
			if (CurrentSessionState.HasFlag(SessionState.HasValidAuthTicket))
				return;

			CurrentSessionState = CurrentSessionState | SessionState.HasValidAuthTicket;

			//Call the event handler if it's not null
			StatusChangedEvent?.Invoke(CurrentSessionState);
		}
	}

	/// <summary>
	/// Serializable <see cref="UnityEvent"/>.
	/// </summary>
	[Serializable]
	public class OnSessionChangedEvent : UnityEvent<SessionState> { }
}

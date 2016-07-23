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
	}
}

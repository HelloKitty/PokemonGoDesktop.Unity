using PokemonGoDesktop.API.Proto;
using PokemonGoDesktop.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.UI
{
	[Serializable]
	public class OnUsernamePlayerDataEvent : UnityEvent<string> { }

	/// <summary>
	/// Observer (and dispatcher) for <see cref="PlayerData"/>.
	/// </summary>
	public class PlayerDataUIObserver : MonoBehaviour, IObserver<PlayerData>
	{
		/// <summary>
		/// Invoked when the observer observers a <see cref="PlayerData"/>.
		/// </summary>
		[SerializeField]
		private OnUsernamePlayerDataEvent OnUsernameUpdate;

		public void OnObserved(PlayerData value)
		{
			OnUsernameUpdate?.Invoke(value.Username);
		}
	}
}

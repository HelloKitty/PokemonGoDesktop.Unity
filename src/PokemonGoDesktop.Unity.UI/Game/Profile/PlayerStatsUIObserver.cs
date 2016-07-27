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
	/// <summary>
	/// Observer (and dispatcher) for <see cref="PlayerStats"/>.
	/// </summary>
	public class PlayerStatsUIObserver : MonoBehaviour, IObserver<PlayerStats>
	{
		[Serializable]
		public class OnExpPercentageCompletedChangedEvent : UnityEvent<float> { }

		[Serializable]
		public class OnLevelChangedEvent : UnityEvent<string> { }

		[SerializeField]
		private OnExpPercentageCompletedChangedEvent OnExpPercentageCompletedChanged;

		[SerializeField]
		private OnLevelChangedEvent OnLevelChanged;

		public void OnObserved(PlayerStats value)
		{
			//compute the new percentage EXP
			float percentageComplete = ((float)value.Experience) / ((float)(value.NextLevelXp - value.PrevLevelXp));

#if DEBUG || DEBUGBUILD
			Debug.Log($"Experience {value.Experience} with Prev {value.PrevLevelXp} with Next {value.NextLevelXp}.");
#endif
			//Broadcast the percentage
			OnExpPercentageCompletedChanged?.Invoke(percentageComplete);

			//Also post the level
			OnLevelChanged?.Invoke(value.Level.ToString());
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.UI
{
	/// <summary>
	/// Component that manages and drives the loading bar forward.
	/// </summary>
	public class LoadingBarDriver : MonoBehaviour
	{
		[Serializable]
		public class OnActionPercentageCompletedChangedEvent : UnityEvent<float> { }

		[SerializeField]
		private UnityEvent OnLoadingComplete;

		/// <summary>
		/// Indicates the total number of expected actions before the game is fully loaded.
		/// </summary>
		[SerializeField]
		public int totalActionsRequiredForFullLoad;

		/// <summary>
		/// Indicates the number of actions that have been completed.
		/// </summary>
		public int CurrentTotalActionsCompleted { get; private set; }

		[SerializeField]
		private OnActionPercentageCompletedChangedEvent OnActionPercentageCompletedChanged;

		private void Start()
		{
			//We start it here because then we don't have to check values in Post or manage state.
			//It starts on Start and ends when the loading is complete
			StartCoroutine(LoadingBarSmoothLoad());
		} 

		public void PostActionCompleted(int count)
		{
			CurrentTotalActionsCompleted += count;
		}

		private IEnumerator LoadingBarSmoothLoad()
		{
			float currentSmoothedLoadValue = 0.0f;

			while(CurrentTotalActionsCompleted < totalActionsRequiredForFullLoad)
			{
				//if (CurrentTotalActionsCompleted < totalActionsRequiredForFullLoad)
					currentSmoothedLoadValue = Mathf.Lerp(currentSmoothedLoadValue, CurrentTotalActionsCompleted, Time.deltaTime); //This isn't how a lerp is suppose to work but it'll smooth for us this way
				//else
				//	currentSmoothedLoadValue += (Time.deltaTime / 5); //we want to stay smooth the whole way, and reach the end, but we can't trust a float to get us there with a lerp

				OnActionPercentageCompletedChanged?.Invoke(currentSmoothedLoadValue);
				yield return null;
			}

			while(currentSmoothedLoadValue < totalActionsRequiredForFullLoad)
			{
				currentSmoothedLoadValue += 0.05f;
				OnActionPercentageCompletedChanged?.Invoke(currentSmoothedLoadValue);
				yield return null;
			}

			//This mean loading ended; dispatch end
			//If enough actions have posted then we should broadcast a completed loading
			if (CurrentTotalActionsCompleted >= totalActionsRequiredForFullLoad)
				OnLoadingComplete?.Invoke();
		}
	}
}

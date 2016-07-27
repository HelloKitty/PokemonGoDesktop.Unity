using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using PokemonGoDesktop.Unity.Common;
using PokemonGoDesktop.Unity.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.Client
{
	[Serializable]
	public class OnPlayerInventoryResponseEvent : UnityEvent<GetInventoryResponse> { }

	[Serializable]
	public class OnPlayerStatsEvent : UnityEvent<PlayerStats> { }

	/// <summary>
	/// Inventory <see cref="RequestComponent"/> Unity3D behaviour.
	/// Sends the request and handles the response.
	/// </summary>
	public class GetInventoryComponent : RequestComponent, IResponseCallbackTargetable<GetInventoryResponse>, IRequestSender
	{
		[SerializeField]
		private OnPlayerStatsEvent OnPlayerStats;

		//TODO: Create an error handling service
		public void OnResponse(GetInventoryResponse response)
		{
			if (response.Success)
			{
				//Right now we just do player stats
				PlayerStats stats = response.InventoryDelta.InventoryItems.Where(x => x.InventoryItemData?.PlayerStats != null)
					.Select(x => x.InventoryItemData.PlayerStats).First();
#if DEBUG || DEBUGBUILD
				Debug.Log($"Recieved PlayerData response with level {stats.Level}.");
#endif
				OnPlayerStats?.Invoke(stats);
			}			
#if DEBUG || DEBUGBUILD
			else
				Debug.Log($"Failed to get {nameof(GetInventoryComponent)} from server. Use error logging middleware for more info.");
#endif
		}

		public void SendRequest()
		{
			//TODO: Fill in parameters for the message
			requestService.SendRequest(new Request() { RequestType = RequestType.GetInventory, RequestMessage = new GetInventoryMessage().ToByteString() }.PackInEnvelope(), this);
		}
	}
}

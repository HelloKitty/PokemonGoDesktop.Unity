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
	public class OnPlayerDataRecievedEvent : UnityEvent<PlayerData> { }

	/// <summary>
	/// Player profile <see cref="RequestComponent"/> Unity3D behaviour.
	/// Sends the request and handles the response.
	/// </summary>
	public class GetPlayerProfileComponent : RequestComponent, IResponseCallbackTargetable<GetPlayerResponse>, IRequestSender
	{
		[SerializeField]
		private OnPlayerDataRecievedEvent OnPlayerDataRecieved;

		//TODO: Create an error handling service
		public void OnResponse(GetPlayerResponse response)
		{
			if (response.Success)
			{
#if DEBUG || DEBUGBUILD
				Debug.Log($"Recieved PlayerData response with username {response.PlayerData.Username}.");
#endif
				OnPlayerDataRecieved?.Invoke(response.PlayerData);
			}			
#if DEBUG || DEBUGBUILD
			else
				Debug.Log($"Failed to get {nameof(GetPlayerProfileResponse)} from server. Use error logging middleware for more info.");
#endif
		}

		public void SendRequest()
		{
			requestService.SendRequest(new Request() { RequestType = RequestType.GetPlayer, RequestMessage = new GetPlayerMessage().ToByteString() }.PackInEnvelope(), this);
		}
	}
}

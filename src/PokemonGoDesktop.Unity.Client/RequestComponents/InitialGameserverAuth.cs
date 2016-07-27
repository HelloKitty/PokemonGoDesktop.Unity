using Easyception;
using Google.Protobuf;
using PokemonGoDesktop.API.Client.Services;
using PokemonGoDesktop.API.Proto;
using PokemonGoDesktop.API.Proto.Services;
using PokemonGoDesktop.Unity.HTTP;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonGoDesktop.Unity.Common
{
	[Serializable]
	public class OnAuthTicketRecievedEvent : UnityEvent<AuthTicketContainer> { }

	[Serializable]
	public class OnErrorWithAuthTicketEvent : UnityEvent<string> { }

	/// <summary>
	/// Components for initial authoration to the Pokemon Go gameserver.
	/// </summary>
	[Injectee]
	public class InitialGameserverAuth : RequestComponent //can't make this a response callback because it's special and accesses the ResponseEnevelope
	{
		/// <summary>
		/// Invoked when the <see cref="AuthTicket"/> is recieved.
		/// </summary>
		[SerializeField]
		private OnAuthTicketRecievedEvent OnAuthTicketRecieved;

		/// <summary>
		/// invoked when an error getting the <see cref="AuthTicket"/> occurs.
		/// </summary>
		[SerializeField]
		private OnErrorWithAuthTicketEvent OnErrorWithAuthTicket;

		//another try authenticate method
		public void TryAuthenticate()
		{
			//TODO: Find out if any of this is nessecary
			RequestEnvelope envelope = new RequestEnvelope()
				.WithMessage(new Request() { RequestType = RequestType.GetPlayerProfile })
				.WithMessage(new Request() { RequestType = RequestType.GetHatchedEggs, RequestMessage = new GetPlayerMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.GetInventory, RequestMessage = new GetInventoryMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.CheckAwardedBadges, RequestMessage = new CheckAwardedBadgesMessage().ToByteString() })
				.WithMessage(new Request() { RequestType = RequestType.DownloadSettings, RequestMessage = new DownloadSettingsMessage() { Hash = "4a2e9bc330dae60e7b74fc85b98868ab4700802e" }.ToByteString() });

			requestService.SendRequest(envelope, OnResponseRecieved);
		}

		private void OnResponseRecieved(ResponseEnvelope envelope)
		{
			if(envelope.AuthTicket == null)
			{
				OnErrorWithAuthTicket?.Invoke($"Unable to generate AuthTicket: {envelope.Error}.");
#if DEBUG || DEBUGBUILD
				Debug.Log($"Failed to be issued an AuthTicket: {envelope.Error}.");
#endif
				return;
			}

#if DEBUG || DEBUGBUILD
			Debug.Log($"Recieved auth response API URL: {envelope.ApiUrl}");
#endif

			//dispatch the AuthTicket to any listeners
			OnAuthTicketRecieved?.Invoke(new AuthTicketContainer(envelope.AuthTicket, envelope.ApiUrl));
		}
	}
}

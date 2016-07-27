using Easyception;
using Google.Protobuf;
using PokemonGoDesktop.API.Proto;
using PokemonGoDesktop.Unity.HTTP;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.Common
{
	/// <summary>
	/// Base class for components that send requests.
	/// </summary>
	[Injectee]
	public class RequestComponent : MonoBehaviour
	{
		/// <summary>
		/// Injected request service.
		/// </summary>
		[Inject]
		private readonly IAsyncUserNetworkRequestService _requestService;

		/// <summary>
		/// The <see cref="IAsyncUserNetworkRequestService"/> to send <see cref="RequestEnvelope"/>s.
		/// </summary>
		protected IAsyncUserNetworkRequestService requestService { get { return _requestService; } }

		private void Start() //check in start; not Awake
		{
			Throw<ArgumentNullException>.If.IsNull(requestService)
				?.Now(nameof(requestService), $"Must have a non-null {nameof(IAsyncUserNetworkRequestService)} in {this.GetType().Name}.");

			OnStart();
		}

		/// <summary>
		/// Replacement for the now internally controlled <see cref="Start"/> Unity3D function.
		/// </summary>
		protected virtual void OnStart()
		{
			//Override if you need to do something when Start is called.
		}
	}
}

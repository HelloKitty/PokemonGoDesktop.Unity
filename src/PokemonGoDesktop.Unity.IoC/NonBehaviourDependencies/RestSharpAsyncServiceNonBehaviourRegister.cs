using PokemonGoDesktop.Unity.HTTP;
using PokemonGoDesktop.Unity.HTTP.RestSharp;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.IoC
{
	public class RestSharpAsyncServiceNonBehaviourRegister : NonBehaviourDependency
	{
		[SerializeField]
		private string BaseUrl;

		//we can create the URL/URI config as inspector settings.
		public override void Register(IServiceRegister register)
		{
			//TODO: Provide URL/URI or RPC URL to the service when I figure it out
			register.Register(new RestSharpAsyncNetworkRequestService(BaseUrl), this.getFlags(), typeof(IAsyncNetworkRequestService));
		}
	}
}

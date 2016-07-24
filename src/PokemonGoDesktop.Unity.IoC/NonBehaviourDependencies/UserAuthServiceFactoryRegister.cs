using PokemonGoDesktop.API.Common;
using PokemonGoDesktop.Unity.Client;
using PokemonGoDesktop.Unity.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.IoC
{
	/// <summary>
	/// Registers a <see cref="IUserAuthServiceFactory"/>.
	/// </summary>
	public class UserAuthServiceFactoryRegister : NonBehaviourDependency
	{
		//Like IoC config
		/// <summary>
		/// Inspector set endpoint/url details for PTC login.
		/// </summary>
		[SerializeField]
		private LoginUrlDetails ptcLoginUrlDetails;

		/// <summary>
		/// Inspector set endpoint/url details for Google login.
		/// </summary>
		[SerializeField]
		private LoginUrlDetails googleLoginUrlDetails;

		public override void Register(IServiceRegister register)
		{
#if DEBUG || DEBUGBUILD
			Debug.Log(ptcLoginUrlDetails.ToString());
#endif
			//create the details map
			Dictionary<AuthType, LoginUrlDetails> authDetailsMap = new Dictionary<AuthType, LoginUrlDetails>()
			{
				{AuthType.PTC, ptcLoginUrlDetails },
				{AuthType.Google, googleLoginUrlDetails }
			};

			UserAuthServiceFactory factory = new UserAuthServiceFactory(authDetailsMap);

			register.Register(factory, getFlags(), typeof(IUserAuthServiceFactory));
		}
	}
}

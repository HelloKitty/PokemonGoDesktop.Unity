using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGoDesktop.Unity.Common
{
	[Serializable]
	public class LoginUrlDetails
	{
		[SerializeField]
		private string _LoginUrl;
		public string LoginUrl { get { return _LoginUrl; } }

		[SerializeField]
		private string _OAuthUrl;
		public string OAuthUrl { get { return _OAuthUrl; } }
	}
}

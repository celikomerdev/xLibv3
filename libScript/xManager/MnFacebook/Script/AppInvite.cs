#if xLibv2
#if Facebook
using System;
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class AppInvite : BaseActiveM
	{
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.Mobile.AppInvite
			(
				new Uri("https://play.google.com/store/apps/details?id=com.xProject.GreenGate"),
				new Uri("https://s1.eksiup.com/ed0ddada249.png"),
				Callback
			);
		}
		
		private void Callback(IAppInviteResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
		}
	}
}
#else
namespace xLib.xFacebook
{
	public class AppInvite : BaseActiveM
	{
		public void Call(){}
	}
}
#endif
#endif
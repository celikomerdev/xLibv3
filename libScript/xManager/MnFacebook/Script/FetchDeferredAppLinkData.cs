﻿#if xLibv2
#if Facebook
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class FetchDeferredAppLinkData : BaseActiveM
	{
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.Mobile.FetchDeferredAppLinkData(Callback);
		}
		
		private void Callback(IAppLinkResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
		}
	}
}
#else
namespace xLib.xFacebook
{
	public class FetchDeferredAppLinkData : BaseActiveM
	{
		public void Call(){}
	}
}
#endif
#endif
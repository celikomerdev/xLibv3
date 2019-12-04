#if xLibv2
#if Facebook
using System;
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class FeedShare : BaseActiveM
	{
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.FeedShare
			(
				"",
				new Uri("https://play.google.com/store/apps/details?id=com.xProject.GreenGate"),
				"linkName",
				"linkCaption",
				"linkDescription",
				new Uri("https://s1.eksiup.com/ed0ddada249.png"),
				"https://youtu.be/D1EC-SKa7nM",
				Callback
			);
		}
		
		private void Callback(IShareResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
			
			
			if (result.Cancelled || !String.IsNullOrEmpty(result.Error))
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":Error:{0}",result.Error);
			}
			else if (!String.IsNullOrEmpty(result.PostId))
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":PostId:{0}",result.PostId);
			}
			else
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":Success");
			}
		}
	}
}
#else
namespace xLib.xFacebook
{
	public class FeedShare : BaseActiveM
	{
		public void Call(){}
	}
}
#endif
#endif
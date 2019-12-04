#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderSocial : HitBuilder<HitBuilderSocial>
	{
		private string socialNetwork = "";
		private string socialAction = "";
		private string socialTarget = "";
		
		internal string GetSocialNetwork()
		{
			return socialNetwork;
		}
		
		internal HitBuilderSocial SetSocialNetwork(string socialNetwork)
		{
			if (socialNetwork != null)
			{
				this.socialNetwork = socialNetwork;
			}
			return this;
		}
		
		internal string GetSocialAction()
		{
			return socialAction;
		}
		
		internal HitBuilderSocial SetSocialAction(string socialAction)
		{
			if (socialAction != null)
			{
				this.socialAction = socialAction;
			}
			return this;
		}
		
		internal string GetSocialTarget()
		{
			return socialTarget;
		}
		
		internal HitBuilderSocial SetSocialTarget(string socialTarget)
		{
			if (socialTarget != null)
			{
				this.socialTarget = socialTarget;
			}
			return this;
		}
		
		internal override HitBuilderSocial GetThis()
		{
			return this;
		}
		
		internal override HitBuilderSocial Validate()
		{
			bool isNull = false;
			
			// if(xApp.CanDebug)
			// {
			// 	Debug.LogFormat("HitBuilderSocial:socialNetwork:{0}",socialNetwork);
			// 	Debug.LogFormat("HitBuilderSocial:socialAction:{0}",socialAction);
			// 	Debug.LogFormat("HitBuilderSocial:socialTarget:{0}",socialTarget);
			// }
			
			if (string.IsNullOrEmpty(socialNetwork))
			{
				Debug.LogWarningFormat("HitBuilderSocial:socialNetwork:null");
				isNull = true;
			}
			
			if (string.IsNullOrEmpty(socialAction))
			{
				Debug.LogWarningFormat("HitBuilderSocial:socialAction:null");
				isNull = true;
			}
			
			if (string.IsNullOrEmpty(socialTarget))
			{
				Debug.LogWarningFormat("HitBuilderSocial:socialTarget:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
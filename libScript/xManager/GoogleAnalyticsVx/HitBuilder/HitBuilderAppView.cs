#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderAppView : HitBuilder<HitBuilderAppView>
	{
		private string screenName = "";
		
		internal string GetScreenName()
		{
			return screenName;
		}
		
		internal HitBuilderAppView SetScreenName(string screenName)
		{
			if (screenName != null)
			{
				this.screenName = screenName;
			}
			return this;
		}
		
		internal override HitBuilderAppView GetThis()
		{
			return this;
		}
		
		internal override HitBuilderAppView Validate()
		{
			bool isNull = false;
			
			if (string.IsNullOrEmpty(screenName))
			{
				Debug.LogWarningFormat("HitBuilderAppView:screenName:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
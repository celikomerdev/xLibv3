#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderTiming : HitBuilder<HitBuilderTiming>
	{
		private string timingCategory = "";
		private long timingInterval;
		private string timingName = "";
		private string timingLabel = "";
		
		internal string GetTimingCategory()
		{
			return timingCategory;
		}
		
		internal HitBuilderTiming SetTimingCategory(string timingCategory)
		{
			if (timingCategory != null)
			{
				this.timingCategory = timingCategory;
			}
			return this;
		}
		
		internal long GetTimingInterval()
		{
			return timingInterval;
		}
		
		internal HitBuilderTiming SetTimingInterval(long timingInterval)
		{
			this.timingInterval = timingInterval;
			return this;
		}
		
		internal string GetTimingName()
		{
			return timingName;
		}
		
		internal HitBuilderTiming SetTimingName(string timingName)
		{
			if (timingName != null)
			{
				this.timingName = timingName;
			}
			return this;
		}
		
		internal string GetTimingLabel()
		{
			return timingLabel;
		}
		
		internal HitBuilderTiming SetTimingLabel(string timingLabel)
		{
			if (timingLabel != null)
			{
				this.timingLabel = timingLabel;
			}
			return this;
		}
		
		internal override HitBuilderTiming GetThis()
		{
			return this;
		}
		
		internal override HitBuilderTiming Validate()
		{
			bool isNull = false;
			
			// if(xApp.CanDebug)
			// {
			// 	Debug.LogFormat("HitBuilderTiming:timingCategory:{0}",timingCategory);
			// 	Debug.LogFormat("HitBuilderTiming:timingInterval:{0}",timingInterval);
			// }
			
			if (string.IsNullOrEmpty(timingCategory))
			{
				Debug.LogWarningFormat("HitBuilderTiming:timingCategory:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
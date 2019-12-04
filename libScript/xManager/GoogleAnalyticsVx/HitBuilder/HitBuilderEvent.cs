#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderEvent : HitBuilder<HitBuilderEvent>
	{
		private string eventCategory = "";
		private string eventAction = "";
		private string eventLabel = "";
		private string eventValue = "0";
		
		internal string GetEventCategory()
		{
			return eventCategory;
		}
		
		internal HitBuilderEvent SetEventCategory(string eventCategory)
		{
			if (eventCategory != null)
			{
				this.eventCategory = eventCategory;
			}
			return this;
		}
		
		internal string GetEventAction()
		{
			return eventAction;
		}
		
		internal HitBuilderEvent SetEventAction(string eventAction)
		{
			if (eventAction != null)
			{
				this.eventAction = eventAction;
			}
			return this;
		}
		
		internal string GetEventLabel()
		{
			return eventLabel;
		}
		
		internal HitBuilderEvent SetEventLabel(string eventLabel)
		{
			if (eventLabel != null)
			{
				this.eventLabel = eventLabel;
			}
			return this;
		}
		
		internal string GetEventValue()
		{
			return eventValue;
		}
		
		internal HitBuilderEvent SetEventValue(string eventValue)
		{
			this.eventValue = eventValue;
			return this;
		}
		
		internal override HitBuilderEvent GetThis()
		{
			return this;
		}
		
		internal override HitBuilderEvent Validate()
		{
			bool isNull = false;
			
			// if(xApp.CanDebug)
			// {
			// 	Debug.LogFormat("HitBuilderEvent:eventCategory:{0}",eventCategory);
			// 	Debug.LogFormat("HitBuilderEvent:eventAction:{0}",eventAction);
			// 	Debug.LogFormat("HitBuilderEvent:eventLabel:{0}",eventLabel);
			// 	Debug.LogFormat("HitBuilderEvent:eventValue:{0}",eventValue);
			// }
			
			if (string.IsNullOrEmpty(eventCategory))
			{
				Debug.LogWarningFormat("HitBuilderEvent:eventCategory:null");
				isNull = true;
			}
			if (string.IsNullOrEmpty(eventAction))
			{
				Debug.LogWarningFormat("HitBuilderEvent:eventCategory:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
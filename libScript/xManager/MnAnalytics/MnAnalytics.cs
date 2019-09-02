#if xLibv3
using UnityEngine;

namespace xLib
{
	public class MnAnalytics : SingletonM<MnAnalytics>
	{
		protected override void Started()
		{
			// SetListen(true);
		}
		
		protected override void OnDestroyed()
		{
			// SetListen(false);
		}
		
		public void AnalyticsSend()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":AnalyticsSend");
			// StAnalytics.AnalyticsSend();
		}
		
		public void LogScreen(string valueName)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LogScreen:{0}",valueName);
		}
		
		public void LogEvent(string category,string action,string label,string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LogEvent:{0}:{1}:{2}:{3}",category,action,label,value);
		}
	}
}
#endif
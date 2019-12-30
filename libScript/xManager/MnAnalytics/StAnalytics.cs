#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public static class StAnalytics
	{
		public static void LogScreen(string key,bool canSend=true)
		{
			if(!canSend) return;
			if(!MnAnalytics.ins) return;
			MnAnalytics.ins.LogScreen(key);
		}
		
		public static void LogEvent(string key="",string label="",double digit=0,Dictionary<string,object> data=null,bool canSend=true)
		{
			if(!canSend) return;
			if(!MnAnalytics.ins) return;
			if(data==null) data = new Dictionary<string,object>();
			MnAnalytics.ins.LogEvent(key,label,digit,data);
		}
		
		public static void LogPurchase(string key="",Dictionary<string, object> data=null,bool canSend=true)
		{
			if(!canSend) return;
			if(!MnAnalytics.ins) return;
			if(data==null) data = new Dictionary<string,object>();
			MnAnalytics.ins.LogPurchase(key,data);
		}
	}
}
#endif
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
		
		public static void LogEvent(string group="group",string key="",double digit=0,string data="",Dictionary<string,object> dict=null,bool canSend=true)
		{
			if(!canSend) return;
			if(!MnAnalytics.ins) return;
			MnAnalytics.ins.LogEvent(group,key,digit,data,dict);
		}
		
		public static void LogPurchase(string key="",Dictionary<string, object> dict=null,bool canSend=true)
		{
			if(!canSend) return;
			if(!MnAnalytics.ins) return;
			MnAnalytics.ins.LogPurchase(key,dict);
		}
	}
}
#endif
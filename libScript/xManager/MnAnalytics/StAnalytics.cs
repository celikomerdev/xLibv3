#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public static class StAnalytics
	{
		public static void LogScreen(string key,bool canSend=true)
		{
			if(!canSend) return;
			// if(!MnAnalytics.ins) return;
			// MnAnalytics.ins.LogScreen(key);
		}
		
		public static void LogEvent(string key="",string label="",object data=null,double digit=0,bool canSend=true)
		{
			if(!canSend) return;
			// if(!MnAnalytics.ins) return;
			// MnAnalytics.ins.LogEvent(key,parameters);
		}
		
		// public static void LogPurchase(string sku,double price,string currency,string receipt,bool canSend=true)
		public static void LogPurchase(string key,Dictionary<string, object> parameters,bool canSend=true)
		{
			if(!canSend) return;
			// if(!MnAnalytics.ins) return;
			// MnAnalytics.ins.LogPurchase(key,parameters);
		}
	}
}
#endif
﻿#if xLibv3
namespace xLib
{
	public static class StAnalytics
	{
		public static void LogScreen(string valueName)
		{
			if(!MnAnalytics.ins) return;
			MnAnalytics.ins.LogScreen(valueName);
		}
		
		// public delegate void LogEventDeletage(string category="",string action="",string label="",string value="0");
		// public static event LogEventDeletage logEventDeletage = new LogEventDeletage();
		public static void LogEvent(string category="",string action="",string label="",string value="0")
		{
			if(!MnAnalytics.ins) return;
			MnAnalytics.ins.LogEvent(category,action,label,value);
		}
		
		public static void LogPurchase(string sku,double price,string currency,string receipt)
		{
			// GGAnalytics.instance.LogPurchase(args.purchasedProduct.definition.storeSpecificId, (double)args.purchasedProduct.metadata.localizedPrice, args.purchasedProduct.metadata.isoCurrencyCode, args.purchasedProduct.receipt);
		}
		
		// internal static List<IAnalyticsSend> arrayAnalytics = new List<IAnalyticsSend>();
		// internal static void AnalyticsSend()
		// {
		// 	for (int i = 0; i < arrayAnalytics.Count; i++)
		// 	{
		// 		arrayAnalytics[i].AnalyticsSend();
		// 	}
		// 	arrayAnalytics.Clear();
		// }
	}
}
#endif
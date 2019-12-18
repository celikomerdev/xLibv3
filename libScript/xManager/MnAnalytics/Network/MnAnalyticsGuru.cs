using System.Collections.Generic;

namespace xLib.xAnalytics
{
	public class MnAnalyticsGuru : SingletonM<MnAnalyticsGuru>
	{
		private Dictionary<string, object> Stamp(Dictionary<string, object> dict)
		{
			// dict["playerLevel"] = DataController.ins.level.Value;
			// dict["coin_total"] = DataController.ins.coin.Value;
			// dict["ticket_total"] = DataController.ins.ticket.Value;
			return dict;
		}
		
		protected override void Started()
		{
			#if GuruAnalytics
			List<ABTest> listTestGroup = new List<ABTest>();
			ABTest levelLockTest = new ABTest(0.5f, "levelLock", 1);		listTestGroup.Add(levelLockTest);
			ABTest startMotoTest = new ABTest(0.5f, "startMoto", 1);		listTestGroup.Add(startMotoTest);
			ABTest defaultCamera = new ABTest(1.0f, "defaultCamera", 4);	listTestGroup.Add(defaultCamera);
			ABTest disableIntro = new ABTest(0.5f, "disableIntro", 1);		listTestGroup.Add(disableIntro);
			Gameguru.Analytics.Builder.SetLogLevel(LogLevel.DEBUG).SetAbTests(listTestGroup).Build();
			#endif
		}
		
		public void LogScreen(string screenName)
		{
			#if GuruAnalytics
			Analytics.LogScreen(screenName);
			#endif
		}
		
		public void LogEvent(string eventName, Dictionary<string, object> parameters)
		{
			#if GuruAnalytics
			Gameguru.Analytics.LogEvent(eventName, Stamp(parameters));
			#endif
		}
		
		public void LogPurchase(string sku, double price, string currency, string receipt, Dictionary<string, object> parameters)
		{
			#if GuruAnalytics
			Gameguru.Analytics.LogPurchase(sku, price, currency, receipt, Stamp(parameters));
			#endif
		}
	}
}
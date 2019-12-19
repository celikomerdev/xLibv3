#if xLibv3
#if AnalyticsGuru
using System.Collections.Generic;
using Gameguru.Analytics;
using UnityEngine;

namespace xLib.xAnalytics
{
	public class MnAnalyticsGuru : SingletonM<MnAnalyticsGuru>
	{
		[SerializeField]private LogLevel logLevel = LogLevel.FATAL;
		
		//TODO inspector
		private Dictionary<string, object> Stamp(Dictionary<string, object> dict)
		{
			// dict["playerLevel"] = DataController.ins.level.Value;
			// dict["coin_total"] = DataController.ins.coin.Value;
			// dict["ticket_total"] = DataController.ins.ticket.Value;
			return dict;
		}
		
		protected override void OnEnabled()
		{
			Init();
		}
		
		protected override void OnDisabled()
		{
			
		}
		
		private static bool isInit = false;
		public override void Init()
		{
			base.Init();
			if(isInit) return;
			isInit = true;
			
			//TODO inspector
			List<ABTest> listTestGroup = new List<ABTest>();
			ABTest levelLockTest = new ABTest(0.5f, "levelLock", 1);		listTestGroup.Add(levelLockTest);
			ABTest startMotoTest = new ABTest(0.5f, "startMoto", 1);		listTestGroup.Add(startMotoTest);
			ABTest defaultCamera = new ABTest(1.0f, "defaultCamera", 4);	listTestGroup.Add(defaultCamera);
			ABTest disableIntro = new ABTest(0.5f, "disableIntro", 1);		listTestGroup.Add(disableIntro);
			
			Analytics.Builder.SetLogLevel(logLevel).SetAbTests(listTestGroup).Build();
		}
		
		public void LogScreen(string screenName)
		{
			Analytics.LogScreen(screenName);
		}
		
		public void LogEvent(string eventName, Dictionary<string, object> parameters)
		{
			Analytics.LogEvent(eventName, Stamp(parameters));
		}
		
		public void LogPurchase(string sku, double price, string currency, string receipt, Dictionary<string, object> parameters)
		{
			Analytics.LogPurchase(sku, price, currency, receipt, Stamp(parameters));
		}
	}
}
#else
namespace xLib.xAnalytics
{
	public class MnAnalyticsGuru : SingletonM<MnAnalyticsGuru>
	{
		[SerializeField]private LogLevel logLevel = LogLevel.FATAL;
	}
}
#endif
#endif
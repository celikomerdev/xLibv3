#if xLibv3
#if AnalyticsGuru
using System.Collections.Generic;
using Gameguru.Analytics;
using UnityEngine;
using xLib.EventClass;

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
		
		protected override void OnEnabled()
		{
			Init();
			MnAnalytics.logScreen += LogScreen;
			MnAnalytics.logEvent += LogEvent;
			Analytics.AdvertisingIDReceived += AdvertisingIDReceived;
		}
		
		protected override void OnDisabled()
		{
			MnAnalytics.logScreen -= LogScreen;
			MnAnalytics.logEvent -= LogEvent;
			Analytics.AdvertisingIDReceived -= AdvertisingIDReceived;
		}
		
		[SerializeField]private EventString advertisingIDReceived = new EventString();
		private void AdvertisingIDReceived(string value)
		{
			if(string.IsNullOrWhiteSpace(value)) return;
			advertisingIDReceived.Value = value;
		}
		
		public void LogScreen(string key)
		{
			Analytics.LogScreen(key);
		}
		
		public void LogEvent(string key,string label,double digit,Dictionary<string,object> data)
		{
			data["label"] = label;
			data["digit"] = digit;
			Analytics.LogEvent(key,Stamp(data));
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
		[SerializeField]private int logLevel = 3;
		[SerializeField]private EventString advertisingIDReceived = new EventString();
	}
}
#endif
#endif
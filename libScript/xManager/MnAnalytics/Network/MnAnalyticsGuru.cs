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
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private IAnalyticObject[] m_arrayIAnalyticObject = new IAnalyticObject[0];
		
		[SerializeField]private GroupTest[] arrayGroupTest = new GroupTest[0];
		[System.Serializable]public struct GroupTest
		{
			[SerializeField]public string name;
			[SerializeField]public int groupCount;
			[SerializeField]public float percent;
		}
		
		private Dictionary<string, object> Stamp(Dictionary<string, object> dict)
		{
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				IAnalyticObject analyticObject = m_arrayIAnalyticObject[i];
				dict[analyticObject.Name] = analyticObject.AnalyticObject;
			}
			return dict;
		}
		
		private static bool isInit = false;
		protected override void Inited()
		{
			if(isInit) return;
			AnalyticsBuilder builder = Analytics.Builder;
			
			if(!CanDebug) logLevel = LogLevel.FATAL;
			builder.SetLogLevel(logLevel);
			builder.SetEnableAppLogHandling(false);
			
			List<ABTest> listGroupTest = new List<ABTest>();
			foreach (GroupTest item in arrayGroupTest)
			{
				listGroupTest.Add(new ABTest(name:item.name,groupCount:item.groupCount,percent:item.percent));
			}
			builder.SetAbTests(listGroupTest);
			
			builder.SetKochovaAndroidGUID(MnKey.GetValue("Kochava_ID"));
			builder.Build();
			isInit = true;
			
			MnTestGroup.onRefreshGroups.Call();
		}
		
		protected override void OnEnabled()
		{
			Init();
			m_arrayIAnalyticObject = arrayIAnalyticObject.GetGenericsArray<IAnalyticObject>(this);
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
		
		public static int GetGroup(string key)
		{
			if(!isInit) return -1;
			return Analytics.GetGroupForABTest(key);
		}
		
		public void LogScreen(string key)
		{
			if(logLevel<=LogLevel.DEBUG) Debug.Log($"{this.name}:LogScreen:{key}",this);
			Analytics.LogScreen(key);
		}
		
		public void LogEvent(string group,string key,double digit,string data,Dictionary<string,object> dict)
		{
			dict = Stamp(dict);
			dict["key"] = key;
			dict["digit"] = digit;
			dict["data"] = data;
			if(logLevel<=LogLevel.DEBUG) Debug.Log($"{this.name}:LogEvent:{group}:dict:{dict.ToJsonString()}",this);
			Analytics.LogEvent(group,dict);
		}
		
		public void LogPurchase(string sku, double price, string currency, string receipt, Dictionary<string, object> data)
		{
			data = Stamp(data);
			if(logLevel<=LogLevel.DEBUG) Debug.Log($"{this.name}:LogPurchase:{sku}:price:{price}:currency:{currency}:receipt:{price}:receipt:{data.ToJsonString()}",this);
			Analytics.LogPurchase(sku,price,currency,receipt,data);
		}
	}
}
#else
using UnityEngine;
using xLib.EventClass;

namespace xLib.xAnalytics
{
	public class MnAnalyticsGuru : SingletonM<MnAnalyticsGuru>
	{
		[SerializeField]private int logLevel = 3;
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private GroupTest[] arrayGroupTest = new GroupTest[0];
		[System.Serializable]private struct GroupTest
		{
			[SerializeField]internal string name;
			[SerializeField]internal int groupCount;
			[SerializeField]internal float percent;
		}
		[SerializeField]private EventString advertisingIDReceived = new EventString();
		
		public static int GetGroup(string key)
		{
			return 0;
		}
	}
}
#endif
#endif
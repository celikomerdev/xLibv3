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
		
		[SerializeField]private static List<string> listGroupKey = new List<string>();
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
			builder.SetLogLevel(LogLevel.FATAL);
			if(CanDebug) builder.SetLogLevel(logLevel);
			
			listGroupKey = new List<string>();
			List<ABTest> listGroupTest = new List<ABTest>();
			foreach (GroupTest item in arrayGroupTest)
			{
				listGroupKey.Add(item.name);
				listGroupTest.Add(new ABTest(name:item.name,groupCount:item.groupCount,percent:item.percent));
			}
			
			builder.SetAbTests(listGroupTest);
			builder.Build();
			isInit = true;
			
			MnTestGroup.onRefreshGroups.Call();
		}
		
		protected override void OnEnabled()
		{
			Init();
			m_arrayIAnalyticObject = arrayIAnalyticObject.GetGenericsArray<IAnalyticObject>();
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
			if(!isInit) return 0;
			if(!listGroupKey.Contains(key)) return 0;
			return Analytics.GetGroupForABTest(key);
		}
		
		public void LogScreen(string key)
		{
			if(CanDebug) Debug.Log($"{this.name}:LogScreen:{key}",this);
			Analytics.LogScreen(key);
		}
		
		public void LogEvent(string group,string key,double digit,string data,Dictionary<string,object> dict)
		{
			dict = Stamp(dict);
			dict["key"] = key;
			dict["digit"] = digit;
			dict["data"] = data;
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:{group}:dict:{dict.ToJsonString()}",this);
			Analytics.LogEvent(group,dict);
		}
		
		public void LogPurchase(string sku, double price, string currency, string receipt, Dictionary<string, object> data)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogPurchase:{sku}:price:{price}:currency:{currency}:receipt:{price}:receipt:{data.ToJsonString()}",this);
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
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
			isInit = true;
			
			AnalyticsBuilder builder = Analytics.Builder;
			builder.SetLogLevel(LogLevel.WARNING);
			if(CanDebug) builder.SetLogLevel(logLevel);
			builder.Build();
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
		
		public void LogScreen(string key)
		{
			if(CanDebug) Debug.Log($"{this.name}:LogScreen:{key}",this);
			Analytics.LogScreen(key);
		}
		
		public void LogEvent(string key,string label,double digit,Dictionary<string,object> data)
		{
			data = Stamp(data);
			data["label"] = label;
			data["digit"] = digit;
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:{key}:data:{data.ToJsonString()}",this);
			Analytics.LogEvent(key,data);
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
		[SerializeField]private EventString advertisingIDReceived = new EventString();
	}
}
#endif
#endif
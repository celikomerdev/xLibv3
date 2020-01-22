#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace xLib
{
	public class MnAnalytics : SingletonM<MnAnalytics>
	{
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private IAnalyticObject[] m_arrayIAnalyticObject = new IAnalyticObject[0];
		
		private Dictionary<string, object> Stamp(Dictionary<string, object> data)
		{
			if(data==null) data = new Dictionary<string,object>();
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				IAnalyticObject analyticObject = m_arrayIAnalyticObject[i];
				data[analyticObject.Name] = analyticObject.AnalyticObject;
			}
			return data;
		}
		
		protected override void OnEnabled()
		{
			m_arrayIAnalyticObject = arrayIAnalyticObject.GetGenericsArray<IAnalyticObject>();
		}
		
		public static Action<string> logScreen = delegate{};
		public void LogScreen(string key)
		{
			if(CanDebug) Debug.Log($"{this.name}:LogScreen:key:{key}",this);
			logScreen(key);
		}
		
		public static Action<string,string,double,Dictionary<string,object>> logEvent = delegate{};
		public void LogEvent(string key="",string label="",double digit=0,Dictionary<string,object> data=null)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:key:{key}:label:{label}:digit:{digit}:data:{data.ToJsonString()}",this);
			logEvent(key,label,digit,data);
		}
		
		public static Action<string,Dictionary<string,object>> logPurchase = delegate{};
		public void LogPurchase(string key,Dictionary<string, object> data)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogPurchase:key:{key}:receipt:{data.ToJsonString()}",this);
			logPurchase(key,data);
		}
	}
}
#endif
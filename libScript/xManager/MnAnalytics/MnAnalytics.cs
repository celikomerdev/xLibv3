#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class MnAnalytics : SingletonM<MnAnalytics>
	{
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private IAnalyticObject[] m_arrayIAnalyticObject = new IAnalyticObject[0];
		
		private Dictionary<string,object> Stamp(Dictionary<string,object> data)
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
		
		public static System.Action<string> logScreen = delegate{};
		public void LogScreen(string key)
		{
			if(CanDebug) Debug.Log($"{this.name}:LogScreen:key:{key}",this);
			logScreen(key);
		}
		
		public static System.Action<string,string,double,string,Dictionary<string,object>> logEvent = delegate{};
		public void LogEvent(string group="",string key="",double digit=0,string data="",Dictionary<string,object> dict=null)
		{
			dict = Stamp(dict);
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:group:{group}:key:{key}:digit:{digit}:data:{data}:dict:{dict.ToJsonString()}",this);
			logEvent(group,key,digit,data,dict);
		}
		
		public static readonly System.Action<string,Dictionary<string,object>> logPurchase = delegate{};
		public void LogPurchase(string key,Dictionary<string,object> data)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogPurchase:key:{key}:receipt:{data.ToJsonString()}",this);
			logPurchase(key,data);
		}
	}
}
#endif
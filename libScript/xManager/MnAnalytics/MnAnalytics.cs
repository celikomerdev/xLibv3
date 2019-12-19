#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class MnAnalytics : SingletonM<MnAnalytics>
	{
		private Dictionary<string,object> Stamp(Dictionary<string,object> dict)
		{
			return dict;
		}
		
		public static Action<string> logScreen = delegate{};
		public void LogScreen(string key)
		{
			if(CanDebug) Debug.Log($"{this.name}:LogScreen:{key}",this);
			logScreen(key);
		}
		
		public static Action<string,string,double,Dictionary<string,object>> logEvent = delegate{};
		public void LogEvent(string key,string label,double digit,Dictionary<string,object> data)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:{key}:label:{label}:digit:{digit}:data:{data.ToJsonString()}",this);
			logEvent(key,label,digit,data);
		}
		
		public static Action<string,Dictionary<string,object>> logPurchase = delegate{};
		public void LogPurchase(string key,Dictionary<string,object> data)
		{
			data = Stamp(data);
			if(CanDebug) Debug.Log($"{this.name}:LogPurchase:{key}:data:{data.ToJsonString()}",this);
			logPurchase(key,data);
		}
	}
}
#endif
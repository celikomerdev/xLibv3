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
		
		public static Action<string,Dictionary<string,object>> logEvent = delegate{};
		public void LogEvent(string key,Dictionary<string,object> parameters)
		{
			parameters = Stamp(parameters);
			if(CanDebug) Debug.Log($"{this.name}:LogEvent:{key}:{parameters.ToJsonString()}",this);
			logEvent(key,parameters);
		}
		
		public static Action<string,Dictionary<string,object>> logPurchase = delegate{};
		public void LogPurchase(string key,Dictionary<string,object> parameters)
		{
			parameters = Stamp(parameters);
			if(CanDebug) Debug.Log($"{this.name}:LogPurchase:{key}:{parameters.ToJsonString()}",this);
			logPurchase(key,parameters);
		}
	}
}
#endif
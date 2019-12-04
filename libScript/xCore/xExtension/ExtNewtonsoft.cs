﻿#if xLibv3
using Newtonsoft.Json.Linq;
using xLib.xNode.NodeObject;

namespace xLib
{
	public static class ExtNewtonsoft
	{
		public static bool TryGetToken<T>(this JObject jObject,string path,ref T outValue)
		{
			if(jObject == null) return false;
			// if(path.Contains("..")) return defaultValue;
			
			JToken jToken = jObject.SelectToken(path);
			if(jToken == null) return false;
			
			outValue = jToken.ToObject<T>();
			return true;
		}
		
		public static T GetTokenSafe<T>(this JObject jObject,string path,T defaultValue)
		{
			jObject.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
		
		public static bool SetTokenSafe(this JObject jObject,string path,object value)
		{
			bool isChange = true;
			JToken jToken = jObject.SelectToken(path);
			if (jToken == null)
			{
				jToken = jObject;
				string[] segments = path.Split('.');
				foreach(string segment in segments)
				{
					if(jToken[segment]==null) jToken[segment] = new JObject();
					jToken = jToken[segment];
				}
			}
			jToken.Replace(JToken.FromObject(value));
			return isChange;
		}
		
		public static T GetTokenSafe<T>(this MonoJObject jObject,string path,T defaultValue)
		{
			jObject.Value.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
		
		public static T GetTokenSafe<T>(this NodeJObject jObject,string path,T defaultValue)
		{
			jObject.Value.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
	}
}
#endif
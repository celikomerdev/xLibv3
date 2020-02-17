#if xLibv3
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public static class ExtNewtonsoft
	{
		public static bool TryGetToken<T>(this JObject jObject,string path,ref T outValue)
		{
			if(jObject == null) return false;
			if(path.Contains("..")) return false;
			
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
		
		public static bool SetTokenSafe<T>(this JObject jObject,string path,T value)
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
		
		public static bool HasPath(this JObject jObject,string path)
		{
			return (jObject.SelectToken(path) != null);
		}
		
		public static void DeletePath(this JObject jObject,string path)
		{
			jObject.SetTokenSafe(path,new JObject());
		}
		
		public static void SetTokenSafe<T>(this MonoJObject jObject,string path,T value)
		{
			if(jObject.Value.SetTokenSafe(path,value)) jObject.Call();
		}
		public static T GetTokenSafe<T>(this MonoJObject jObject,string path,T defaultValue)
		{
			jObject.Value.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
		
		public static void SetTokenSafe<T>(this NodeJObject jObject,string path,T value)
		{
			if(jObject.Value.SetTokenSafe(path,value)) jObject.Call();
		}
		public static T GetTokenSafe<T>(this NodeJObject jObject,string path,T defaultValue)
		{
			jObject.Value.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
		
		public static string ToJsonString<TKey,TValue> (this IDictionary<TKey,TValue> dictionary)
		{
			return JsonConvert.SerializeObject(dictionary);
		}
		
		public static T FromJsonStringSafe<T>(this T output,string data)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(data);
			}
			catch (System.Exception ex)
			{
				Debug.LogException(new UnityException($"FromJsonString:{ex.Message}:data:{data}",ex));
			}
			return output;
		}
	}
}
#endif
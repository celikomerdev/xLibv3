#if xLibv3
using Newtonsoft.Json.Linq;

namespace xLib
{
	public static class ExtNewtonsoft
	{
		public static bool TryGetToken<T>(this JObject jObject,string path,ref T outValue)
		{
			if(jObject == null) return false;
			// if(path.Contains("..")) return defaultValue;
			
			JToken valueToken = jObject.SelectToken(path);
			if(valueToken == null) return false;
			
			outValue = valueToken.ToObject<T>();
			return true;
		}
		
		public static T GetTokenSafe<T>(this JObject jObject,string path,T defaultValue)
		{
			jObject.TryGetToken(path,ref defaultValue);
			return defaultValue;
		}
		
		public static void SetSafe(this JObject jObject,string path,object value)
		{
			jObject[path] = JToken.FromObject(value);
		}
	}
}
#endif
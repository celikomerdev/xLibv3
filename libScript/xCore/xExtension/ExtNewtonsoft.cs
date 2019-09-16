#if xLibv3
using Newtonsoft.Json.Linq;

namespace xLib
{
	public static class ExtNewtonsoft
	{
		public static T GetValueSafe<T>(this JToken jToken,object key,T defaultValue)
		{
			if(jToken == null) return defaultValue;
			
			JToken valueToken = jToken[key];
			T returnValue = valueToken.Type == JTokenType.Null? defaultValue:valueToken.Value<T>();
			
			return returnValue;
		}
		
		public static T SelectTokenSafe<T>(this JObject jObject,string path,T defaultValue)
		{
			if(jObject == null) return defaultValue;
			JToken valueToken = jObject.SelectToken(path);
			if(valueToken == null) return defaultValue;
			return valueToken.ToObject<T>();
		}
	}
}
#endif

// JToken vehicle = MnConfig.data.SelectToken($"$.arrayVehicle[?(@.id == '{carData.model}')]");
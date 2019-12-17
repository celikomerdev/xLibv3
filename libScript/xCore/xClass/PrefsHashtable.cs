#if xLibv2
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace xLib
{
	public static class PrefsHashtable
	{
		private static Hashtable Values = new Hashtable();
		private static string[] passwords = new string[1]{"password"};
		private static JsonSerializerSettings settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
		
		static PrefsHashtable()
		{
			Values = new Hashtable();
			Load();
		}
		
		public static void Set<T>(string key, T value)
		{
			Values[key] = value;
		}
		
		public static T Get<T>(string key, T defaultValue)
		{
			if (Values.ContainsKey(key))
			{
				return (T)Values[key];
			}
			else
			{
				return defaultValue;
			}
		}
		
		public static bool HasKey(string key)
		{
			return Values.ContainsKey(key);
		}
		
		public static void DeleteKey(string key)
		{
			Values.Remove(key);
		}
		
		public static void DeleteAll()
		{
			Values.Clear();
		}
		
		public static void Save()
		{
			JObject jObject = new JObject();
			jObject.Add("Values",JsonConvert.SerializeObject(Values,settings));
			
			string stringJson = jObject.ToString().EncryptSHA256UTF8(passwords);
			xPersistentData.SetString("PrefsHashtable",stringJson);
		}
		
		public static void Load()
		{
			string stringJson = "";
			stringJson = xPersistentData.GetString("PrefsHashtable");
			stringJson = stringJson.DecryptSHA256UTF8(passwords);
			if(string.IsNullOrWhiteSpace(stringJson)) return;
			
			JObject jObject = JObject.Parse(stringJson);
			JObject ValuesToken = (JObject)jObject.GetValue("Values");
			if(ValuesToken==null) return;
			Values = JsonConvert.DeserializeObject<Hashtable>(ValuesToken.Value<string>(), settings);
		}
		
		public static void Debug()
		{
			string returnValue = "saveObject:";
			foreach(DictionaryEntry pair in Values)
			{
				returnValue += string.Format("\n{0}:{1}",pair.Key,pair.Value);
			}
			xDebug.LogTempFormat(returnValue);
		}
	}
}
#endif
#if xLibv2
using Newtonsoft.Json.Linq;

namespace xLib
{
	public static class PrefsJson
	{
		private static JObject Values = new JObject();
		private static string[] passwords = new string[1]{"password"};
		
		static PrefsJson()
		{
			Load();
		}
		
		public static void Set<T>(string path, T value)
		{
			Values.SetTokenSafe(path,value);
		}
		
		public static T Get<T>(string path, T defaultValue)
		{
			return Values.GetTokenSafe(path,defaultValue);
		}
		
		public static bool HasKey(string path)
		{
			return Values.HasPath(path);
		}
		
		public static void DeleteKey(string path)
		{
			Values.DeletePath(path);
		}
		
		public static void DeleteAll()
		{
			Values.RemoveAll();
		}
		
		public static void Save()
		{
			JObject jObject = new JObject();
			jObject.Add("Values",Values);
			
			string stringJson = jObject.ToString().EncryptSHA256UTF8(passwords);
			xPersistentData.SetString("PrefsJson",stringJson);
		}
		
		public static void Load()
		{
			string stringJson = "";
			stringJson = xPersistentData.GetString("PrefsJson");
			stringJson = stringJson.DecryptSHA256UTF8(passwords);
			if(string.IsNullOrWhiteSpace(stringJson)) return;
			
			JObject jObject = JObject.Parse(stringJson);
			JObject ValuesToken = (JObject)jObject.GetValue("Values");
			if(ValuesToken==null) return;
			Values = ValuesToken;
		}
		
		public static void Debug()
		{
			UnityEngine.Debug.Log(Values);
		}
	}
}
#endif
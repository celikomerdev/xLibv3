#if xLibv3
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class MnKey : SingletonM<MnKey>
	{
		private static Dictionary<string,string> dictionary = new Dictionary<string,string>();
		public TextAsset general;
		public TextAsset android;
		public TextAsset ios;
		public TextAsset editor;
		
		#region Mono
		protected override void Awaked()
		{
			Init();
		}
		#endregion
		
		#region Init
		protected override void Inited()
		{
			dictionary = new Dictionary<string,string>();
			dictionary.Clear();
			
			AddAsset(general);
			
			#if UNITY_ANDROID
			AddAsset(android);
			#elif UNITY_IOS
			AddAsset(ios);
			#endif
			
			#if UNITY_EDITOR
			AddAsset(editor);
			#endif
			
			if(CanDebug) Debug.LogFormat(this,this.name+":Dictionary:{0}",Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
		}
		
		private static void AddAsset(TextAsset value)
		{
			if(!value) return;
			JObject root = JObject.Parse(value.text);
			foreach(KeyValuePair<string,JToken> kvp in root)
			{
				dictionary[kvp.Key] = kvp.Value.Value<string>();
			}
		}
		#endregion
		
		#region Public
		public static string GetValue(string key)
		{
			if(string.IsNullOrWhiteSpace(key)) return "";
			string temp = key;
			dictionary.TryGetValue(key,out temp);
			return temp;
		}
		#endregion
	}
}
#endif
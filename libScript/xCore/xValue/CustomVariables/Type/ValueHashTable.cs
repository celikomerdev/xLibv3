#if xLibv2
using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueHashTable : xValueEqual<Hashtable>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueHashTable global = new ValueHashTable();
			global.Globalize();
		}
		#endregion
		
		protected override void CreateDefault()
		{
			if(value==null) value = new Hashtable();
		}
		
		#region Compare
		protected override bool IsEqual(Hashtable value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
		
		#region Override
		public override string ValueToString
		{
			get
			{
				string returnValue = "Hashtable:";
				foreach(DictionaryEntry pair in Value)
				{
					returnValue += string.Format("--:{0}:{1}:--",pair.Key,pair.Value);
				}
				return returnValue;
			}
		}
		#endregion
		
		[SerializeField]private TypeNameHandling typeNameHandling = TypeNameHandling.Auto;
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				JsonSerializerSettings settings = new JsonSerializerSettings {TypeNameHandling = typeNameHandling};
				return JsonConvert.SerializeObject(Value,settings);
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				
				JsonSerializerSettings settings = new JsonSerializerSettings {TypeNameHandling = typeNameHandling};
				Value = JsonConvert.DeserializeObject<Hashtable>(stringJson,settings);
			}
		}
		#endregion
	}
}
#endif
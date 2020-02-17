#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueJObject : xValueEqual<JObject>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueJObject global = new ValueJObject();
			global.Globalize();
		}
		#endregion
		
		protected override void CreateDefault()
		{
			if(value==null) value = new JObject();
		}
		
		#region Compare
		protected override bool IsEqual(JObject valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				return Value;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				Value = JObject.Parse(stringJson);
			}
		}
		#endregion
	}
}
#endif
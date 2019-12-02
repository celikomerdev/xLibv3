#if xLibv3
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueJObject : xValueEqual<JObject>
	{
		protected override void CreateDefault()
		{
			if(value==null) value = new JObject();
		}
		
		#region Compare
		protected override bool IsEqual(JObject value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
		
		[SerializeField]private TypeNameHandling typeNameHandling = TypeNameHandling.Auto;
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
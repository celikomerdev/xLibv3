#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTextAsset : xValueEqual<TextAsset>
	{
		#region Compare
		protected override bool IsEqual(TextAsset value)
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
				return Value.xHashCode().ToString();
			}
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				string stringData = "";
				if(Value!=null)
				{
					stringData = Value.text;
				}
				JToken jToken;
				jToken = JToken.FromObject(stringData);
				return jToken;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				
				TextAsset textAsset = new TextAsset(stringData);
				Value = textAsset;
			}
		}
		#endregion
	}
}
#endif
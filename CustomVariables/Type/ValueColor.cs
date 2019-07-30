#if xLibv2
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueColor : xValueEqual<Color>
	{
		#region Compare
		protected override bool IsEqual(Color value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
		
		#region Override
		// protected override string ValueString
		// {
		// 	get
		// 	{
		// 		//Color.RGBToHSV(Value,H,S,V)
		// 		ColorHSVA temp = new ColorHSVA();
		// 		temp.Color = Value;
		// 		return temp.HSV.ToString();
		// 	}
		// }
		#endregion
		
		#region ISerializableBase
		internal override object SerializedObject
		{
			get
			{
				string stringData = "#"+ColorUtility.ToHtmlStringRGBA(Value);
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
				
				Color tempValue = Color.white;
				if(ColorUtility.TryParseHtmlString(stringData,out tempValue))
				{
					Value = tempValue;
				}
			}
		}
		#endregion
	}
}
#endif
#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueColor : xValueEqual<Color>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueColor global = new ValueColor();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(Color valueNew)
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
				if(Value.IsNull()) return null;
				if(Value.Equals(ValueDefault)) return null;
				
				string stringData = "#"+ColorUtility.ToHtmlStringRGBA(Value);
				return JToken.FromObject(stringData);
			}
			set
			{
				if(value.IsNull()) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				if(ColorUtility.TryParseHtmlString(stringJson,out Color tempValue))
				{
					Value = tempValue;
				}
			}
		}
		#endregion
	}
}
#endif
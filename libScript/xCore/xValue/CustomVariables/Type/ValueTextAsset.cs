#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTextAsset : xValueEqual<TextAsset>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueTextAsset global = new ValueTextAsset();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(TextAsset valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
		
		#region Override
		public override string ValueToString
		{
			get
			{
				return Value.text;
			}
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				if(Value.IsNull()) return null;
				if(Value.Equals(ValueDefault)) return null;
				
				return JToken.FromObject(Value.text);
			}
			set
			{
				if(value.IsNull()) return;
				string stringJson = value.ToString();
				// if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				Value = new TextAsset(stringJson);
			}
		}
		#endregion
	}
}
#endif
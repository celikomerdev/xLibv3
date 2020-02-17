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
				string stringData = "";
				if(Value!=null)
				{
					stringData = Value.text;
				}

				JToken jToken = JToken.FromObject(stringData);
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
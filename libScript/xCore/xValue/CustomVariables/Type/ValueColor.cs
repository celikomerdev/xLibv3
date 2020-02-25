﻿#if xLibv3
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
				string stringData = "#"+ColorUtility.ToHtmlStringRGBA(Value);
				JToken jToken = JToken.FromObject(stringData);
				return jToken;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				
				if(ColorUtility.TryParseHtmlString(stringData,out Color tempValue))
				{
					Value = tempValue;
				}
			}
		}
		#endregion
	}
}
#endif
#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueString : xValueEqual<string>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueString global = new ValueString();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(string valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
		
		#region Function
		public override string ValueAdd
		{
			set
			{
				Value += value;
			}
		}
		#endregion
		
		#region Override
		public override string ValueToString
		{
			get
			{
				return Value;
			}
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				if(string.IsNullOrWhiteSpace(Value)) return null;
				return JToken.FromObject(Value);
			}
			set
			{
				if(value==null) return;
				JToken jToken = JToken.FromObject(value);
				Value = jToken.ToObject<string>();
			}
		}
		#endregion
	}
}
#endif
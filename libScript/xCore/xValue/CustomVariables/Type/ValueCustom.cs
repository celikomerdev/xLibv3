#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueCustom : xValueEqual<ValueCustom.Token>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueCustom global = new ValueCustom();
			global.Globalize();
		}
		#endregion
		
		#region Token
		[Serializable]
		public struct Token
		{
			public bool valueBool;
			public bool[] arrayBool;
			public int valueInt;
			public float valueFloat;
			public string valueString;
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(ValueCustom.Token valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return valueNew.Equals(Value);
		}
		#endregion
	}
}
#endif
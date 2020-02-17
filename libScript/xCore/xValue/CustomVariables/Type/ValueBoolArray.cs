#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueBoolArray : xValueEqual<bool[]>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueBoolArray global = new ValueBoolArray();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(bool[] valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
	}
}
#endif
#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueIntArray : xValueEqual<int[]>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueIntArray global = new ValueIntArray();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(int[] valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
	}
}
#endif
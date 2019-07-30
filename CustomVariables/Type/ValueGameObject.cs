#if xLibv2
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueGameObject : xValueEqual<GameObject>
	{
		#region Compare
		protected override bool IsEqual(GameObject value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
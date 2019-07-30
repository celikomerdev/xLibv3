#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueMaterial : xValueEqual<Material>
	{
		#region Compare
		protected override bool IsEqual(Material value)
		{
			if(value.xHashCode() != value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
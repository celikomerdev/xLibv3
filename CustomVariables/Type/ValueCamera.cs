#if xLibv2
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueCamera : xValueEqual<Camera>
	{
		#region Compare
		protected override bool IsEqual(Camera value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
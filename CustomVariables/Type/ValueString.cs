#if xLibv2
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueString : xValueEqual<string>
	{
		#region Compare
		protected override bool IsEqual(string value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
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
		protected override string ValueToString
		{
			get
			{
				return Value;
			}
		}
		#endregion
	}
}
#endif
#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueString : xValueEqual<string>
	{
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
	}
}
#endif
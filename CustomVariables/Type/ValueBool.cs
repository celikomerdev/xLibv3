#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueBool : xValueEqual<bool>
	{
		#region Compare
		protected override bool IsEqual(bool value)
		{
			return (value == Value);
		}
		#endregion
		
		
		#region OverrideAnalytics
		internal override string AnalyticString
		{
			get
			{
				return "Bool";
			}
		}
		
		internal override string AnalyticDigit
		{
			get
			{
				return (Value? "1":"0");
			}
		}
		#endregion
	}
}
#endif
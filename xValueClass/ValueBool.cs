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
		protected override string AnalyticLabel
		{
			get
			{
				return "";
			}
		}
		
		protected override string AnalyticValue
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
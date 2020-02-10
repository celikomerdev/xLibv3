#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueLong : xValueThreshold<long>
	{
		#region Compare
		protected override bool IsEqual(long valueNew)
		{
			return (valueNew == Value);
		}
		
		protected override bool IsDefault(long valueNew)
		{
			return (valueNew == ValueDefault);
		}
		
		protected override bool IsThreshold(long valueNew)
		{
			return (valueThreshold > Mathf.Abs(valueNew-Value));
		}
		#endregion
		
		#region Function
		public override long ValueAdd
		{
			set
			{
				Value += value;
			}
		}
		#endregion
		
		
		#region OverrideAnalytics
		internal override string AnalyticString
		{
			get
			{
				return "Long";
			}
		}
		
		internal override double AnalyticDigit
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
#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueLong : xValueThreshold<long>
	{
		#region Compare
		protected override bool IsEqual(long value)
		{
			return (value == Value);
		}
		
		protected override bool IsDefault(long value)
		{
			return (value == ValueDefault);
		}
		
		protected override bool IsThreshold(long value)
		{
			return (valueThreshold > Mathf.Abs(value-Value));
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
				return Value.ToString();
			}
		}
		#endregion
	}
}
#endif
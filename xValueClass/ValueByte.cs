#if xLibv2
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueByte : xValueThreshold<byte>
	{
		#region Compare
		protected override bool IsEqual(byte value)
		{
			return (value == Value);
		}
		
		protected override bool IsDefault(byte value)
		{
			return (value == ValueDefault);
		}
		
		protected override bool IsThreshold(byte value)
		{
			return (valueThreshold > Mathf.Abs(value-Value));
		}
		#endregion
		
		#region Function
		public override byte ValueAdd
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
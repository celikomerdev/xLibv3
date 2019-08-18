#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueInt : xValueThreshold<int>
	{
		#region Compare
		protected override bool IsEqual(int value)
		{
			return (value == Value);
		}
		
		protected override bool IsDefault(int value)
		{
			return (value == ValueDefault);
		}
		
		protected override bool IsThreshold(int value)
		{
			return (valueThreshold > Mathf.Abs(value-Value));
		}
		#endregion
		
		#region Function
		public override int ValueAdd
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
				return "Int";
			}
		}
		
		internal override string AnalyticDigit
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
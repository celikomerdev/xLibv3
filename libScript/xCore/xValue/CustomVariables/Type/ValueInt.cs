#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueInt : xValueThreshold<int>
	{
		#region Compare
		protected override bool IsEqual(int valueNew)
		{
			return (valueNew == Value);
		}
		
		protected override bool IsDefault(int valueNew)
		{
			return (valueNew == ValueDefault);
		}
		
		protected override bool IsThreshold(int valueNew)
		{
			return (valueThreshold > Mathf.Abs(valueNew-Value));
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
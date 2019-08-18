#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueFloat : xValueThreshold<float>
	{
		#region Compare
		protected override bool IsEqual(float value)
		{
			return Mathf.Approximately(value,Value);
		}
		
		protected override bool IsDefault(float value)
		{
			return (value == ValueDefault);
		}
		
		protected override bool IsThreshold(float value)
		{
			return (valueThreshold > Mathf.Abs(value-Value));
		}
		
		public bool cacheGreater;
		protected override bool CanCache(float value)
		{
			if(cacheGreater) return (Mathf.Abs(value) > Mathf.Abs(ValueCache));
			return true;
		}
		#endregion
		
		#region Function
		public override float ValueAdd
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
				return "Float100x";
			}
		}
		
		internal override string AnalyticDigit
		{
			get
			{
				return (Value*100).ToString("F0");
			}
		}
		#endregion
	}
}
#endif
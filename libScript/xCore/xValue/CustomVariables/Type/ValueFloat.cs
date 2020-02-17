#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueFloat : xValueThreshold<float>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueFloat global = new ValueFloat();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(float valueNew)
		{
			return Mathf.Approximately(valueNew,Value);
		}
		
		protected override bool IsDefault(float valueNew)
		{
			return (valueNew == ValueDefault);
		}
		
		protected override bool IsThreshold(float valueNew)
		{
			return (valueThreshold > Mathf.Abs(valueNew-Value));
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
				return "Float";
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
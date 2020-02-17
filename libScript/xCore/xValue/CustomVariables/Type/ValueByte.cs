#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueByte : xValueThreshold<byte>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueByte global = new ValueByte();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(byte valueNew)
		{
			return (valueNew == Value);
		}
		
		protected override bool IsDefault(byte valueNew)
		{
			return (valueNew == ValueDefault);
		}
		
		protected override bool IsThreshold(byte valueNew)
		{
			return (valueThreshold > Mathf.Abs(valueNew-Value));
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
		internal override string AnalyticString
		{
			get
			{
				return "Byte";
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
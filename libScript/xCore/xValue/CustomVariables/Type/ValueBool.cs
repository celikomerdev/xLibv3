#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueBool : xValueEqual<bool>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueBool global = new ValueBool();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(bool valueNew)
		{
			return (valueNew == Value);
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
		
		internal override double AnalyticDigit
		{
			get
			{
				return (Value? 1:0);
			}
		}
		#endregion
	}
}
#endif
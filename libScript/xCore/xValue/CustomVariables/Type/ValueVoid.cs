#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]public struct Void{}
	[Serializable]public class ValueVoid : xValue<Void>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueVoid global = new ValueVoid();
			global.Globalize();
		}
		#endregion
		
		public override string ValueToString
		{
			get
			{
				return "";
			}
		}
	}
}
#endif
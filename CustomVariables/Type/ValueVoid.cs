#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]public struct Void{}
	[Serializable]public class ValueVoid : xValue<Void>
	{
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
#if xLibv2
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathByte
	{
		#region Mod
		public static byte Repeat(byte value,byte lenght)
		{
			return (byte)Mathf.Repeat(value,lenght);
			//while (value >= max) value -= max;
			//while (value < 0) value += max;
			
			//value %= max;
			//if(value < 0) value += max;
			//return value;
		}
		#endregion
	}
}
#endif
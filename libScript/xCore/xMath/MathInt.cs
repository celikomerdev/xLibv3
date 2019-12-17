#if xLibv3
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathInt
	{
		#region Mod
		public static int LerpUnclamped(float a, float b, float t)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped(a,b,t));
		}
		
		public static int Repeat(int value,int lenght)
		{
			return (int)Mathf.Repeat(value,lenght);
			//while (value >= max) value -= max;
			//while (value < 0) value += max;
			
			// value %= max;
			// if(value < 0) value += max;
			// return value;
		}
		#endregion
	}
}
#endif
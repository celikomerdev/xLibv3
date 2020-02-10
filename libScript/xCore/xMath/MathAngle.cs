﻿#if xLibv3
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathAngle
	{
		// public static float ClampAngle(float angle,float min,float max)
		// {
		// 	// angle %= 360;
		// 	angle = MathFloat.Mod(360,angle);
		// 	return Mathf.Clamp(angle,min,max);
		// }
		
		//TODO optimize
		public static float ClampAngle(float angle, float min, float max)
		{
			if (min < 0 && max > 0 && (angle > max || angle < min))
			{
				angle -= 360;
				if (angle > max || angle < min)
				{
					if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
					return max;
				}
			}
			else if(min > 0 && (angle > max || angle < min))
			{
				angle += 360;
				if (angle > max || angle < min)
				{
					if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
					return max;
				}
			}
			
			if (angle < min) return min;
			if (angle > max) return max;
			return angle;
		}
		
		public static float DeltaAngle(float prevAngle,float curAngle)
		{
			float difference = curAngle-prevAngle;
			if(difference>180f)
			{
				difference-=360f;
			}
			else if(difference<-180f)
			{
				difference+=360f;
			}
			return difference;
		}
	}
}
#endif
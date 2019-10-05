#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtVector2
	{
		public static float Angle180(Vector2 center,Vector2 target)
		{
			float angle = Vector2.Angle(Vector2.right,target-center);
			if(center.y > target.y)
			{
				angle = -angle;
			}
			return angle;
		}
		
		public static float Angle360(Vector2 center, Vector2 target)
		{
			float angle = Vector2.Angle(Vector2.right,target-center);
			if(center.y > target.y)
			{
				angle = 360-angle;
			}
			return angle;
		}
		
		// public static Vector2 PyPx(this Vector2 target)
		// {
		// 	Vector2 temp = Vector2.zero;
		// 	temp.x = target.y;
		// 	temp.y = -target.x;
		// 	return temp;
		// }
	}
}
#endif
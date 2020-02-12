#if xLibv2
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathVector2
	{
		public static Vector2 Clamp (Vector2 value, float min = -1, float max = 1)
		{
			value.x = Mathf.Clamp (value.x, min, max);
			value.y = Mathf.Clamp (value.y, min, max);
			return value;
		}
		
		public static Vector2 Clamp (Vector2 value, Vector2 min, Vector2 max)
		{
			value.x = Mathf.Clamp (value.x, min.x, max.x);
			value.y = Mathf.Clamp (value.y, min.y, max.y);
			return value;
		}
		
		public static bool IsClamp(ref Vector2 value, Vector2 min, Vector2 max)
		{
			bool returnValue = false;
			if( MathFloat.IsClamp(ref value.x, min.x, max.x) ) returnValue = true;
			if( MathFloat.IsClamp(ref value.y, min.y, max.y) ) returnValue = true;
			return returnValue;
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathAngle
	{
		#region float
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
		#endregion
		
		#region Vector2
		public static float Angle180(Vector2 center,Vector2 target)
		{
			float angle = Vector2.Angle(Vector2.right,target-center);
			if(center.y > target.y) angle = -angle;
			return angle;
		}
		
		public static float Angle360(Vector2 center,Vector2 target)
		{
			float angle = Vector2.Angle(Vector2.right,target-center);
			if(center.y > target.y) angle = 360-angle;
			return angle;
		}
		
		public static Vector2 LerpAngle (Vector2 from, Vector2 to, float t)
		{
			from.x = Mathf.LerpAngle (from.x, to.x, t);
			from.y = Mathf.LerpAngle (from.y, to.y, t);
			return from;
		}
		
		public static Vector2 SmoothDampAngle (Vector2 current, Vector2 target, ref Vector2 vel, float t)
		{
			current.x = Mathf.SmoothDampAngle (current.x, target.x, ref vel.x, t);
			current.y = Mathf.SmoothDampAngle (current.y, target.y, ref vel.y, t);
			return current;
		}
		#endregion
		
		#region Vector3
		public static Vector3 ClampAngle (Vector3 value, Vector3 min, Vector3 max)
		{
			value.x = MathAngle.ClampAngle (value.x, min.x, max.x);
			value.y = MathAngle.ClampAngle (value.y, min.y, max.y);
			value.z = MathAngle.ClampAngle (value.z, min.z, max.z);
			return value;
		}
		
		public static Vector3 LerpAngle (Vector3 from, Vector3 to, float t)
		{
			from.x = Mathf.LerpAngle (from.x, to.x, t);
			from.y = Mathf.LerpAngle (from.y, to.y, t);
			from.z = Mathf.LerpAngle (from.z, to.z, t);
			return from;
		}
		
		public static Vector3 MoveTowardsAngle (Vector3 from, Vector3 to, float t)
		{
			from.x = Mathf.MoveTowardsAngle (from.x, to.x, t);
			from.y = Mathf.MoveTowardsAngle (from.y, to.y, t);
			from.z = Mathf.MoveTowardsAngle (from.z, to.z, t);
			return from;
		}
		
		public static Vector3 Lerp(Vector3 a, Vector3 b, Vector3 t)
		{
			Vector3 temp = Vector3.zero;
			
			temp.x = Mathf.LerpUnclamped(a.x, b.x, t.x);
			temp.y = Mathf.LerpUnclamped(a.y, b.y, t.y);
			temp.z = Mathf.LerpUnclamped(a.z, b.z, t.z);
			
			return temp;
		}
		
		public static Vector3 SmoothDampAngle (Vector3 current, Vector3 target, ref Vector3 vel, float t)
		{
			current.x = Mathf.SmoothDampAngle (current.x, target.x, ref vel.x, t);
			current.y = Mathf.SmoothDampAngle (current.y, target.y, ref vel.y, t);
			current.z = Mathf.SmoothDampAngle (current.z, target.z, ref vel.z, t);
			return current;
		}
		#endregion
	}
}
#endif
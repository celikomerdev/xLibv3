#if xLibv3
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathVector3
	{
		#region Operation
		public static Vector3 Divide(Vector3 valueA,Vector3 valueB)
		{
			valueA.x /= valueB.x;
			valueA.y /= valueB.y;
			valueA.z /= valueB.z;
			return valueA;
		}
		#endregion
		
		
		#region Vector
		public static Vector3 Clamp (Vector3 value, float min = -1, float max = 1)
		{
			value.x = Mathf.Clamp (value.x, min, max);
			value.y = Mathf.Clamp (value.y, min, max);
			value.z = Mathf.Clamp (value.z, min, max);
			return value;
		}
		
		
		public static Vector3 Clamp (Vector3 value, Vector3 min, Vector3 max)
		{
			value.x = Mathf.Clamp (value.x, min.x, max.x);
			value.y = Mathf.Clamp (value.y, min.y, max.y);
			value.z = Mathf.Clamp (value.z, min.z, max.z);
			return value;
		}
		
		public static bool IsClamp(ref Vector3 value, Vector3 min, Vector3 max)
		{
			bool returnValue = false;
			if(MathFloat.IsClamp(ref value.x,min.x,max.x)) returnValue = true;
			if(MathFloat.IsClamp(ref value.y,min.y,max.y)) returnValue = true;
			if(MathFloat.IsClamp(ref value.z,min.z,max.z)) returnValue = true;
			return returnValue;
		}
		#endregion
		
		public static float Remap(Vector3 a, Vector3 b, Vector3 value)
		{
			Vector3 AB = b-a;
			Vector3 AV = value-a;
			return Vector3.Dot(AV,AB)/Vector3.Dot(AB,AB);
		}
		
		// public static float Ratio(Vector3 value,Vector3 scale)
		// {
		// 	return AbsoluteSum(Vector3.Scale(value,scale))/AbsoluteSum(scale);
		// }
		
		// public static float AbsoluteSum(Vector3 value)
		// {
		// 	float absoluteSum = 0;
		// 	absoluteSum += Mathf.Abs(value.x);
		// 	absoluteSum += Mathf.Abs(value.y);
		// 	absoluteSum += Mathf.Abs(value.z);
		// 	return absoluteSum;
		// }
	}
}
#endif
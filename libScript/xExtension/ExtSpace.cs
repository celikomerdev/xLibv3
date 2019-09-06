#if xLibv3
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class ExtSpace
	{
		public static Vector3 origin = Vector3.zero;
		
		private static UnityAction<Vector3> listener = delegate(Vector3 arg){};
		public static void Listener(UnityAction<Vector3> call,bool addition)
		{
			if(addition) listener += call;
			else listener -= call;
		}
		
		public static void Translate(Vector3 value)
		{
			if(xDebug.CanDebug) Debug.LogFormat("Translate:{0}",value.ToString());
			origin += value;
			listener.Invoke(value);
		}
		
		public static Vector3 SpacePositionGet(this Transform trans)
		{
			return trans.position-origin;
		}
		
		public static void SpacePositionSet(this Transform trans,Vector3 value)
		{
			trans.position = origin+value;
		}
		
		#if ModPhysics
		public static Vector3 SpacePositionGet(this Rigidbody trans)
		{
			return trans.position-origin;
		}
		
		public static void SpacePositionMove(this Rigidbody trans,Vector3 value)
		{
			trans.MovePosition(origin+value);
		}
		#endif
	}
}
#endif
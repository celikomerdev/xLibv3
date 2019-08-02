#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtSpace
	{
		public static Vector3 SpacePositionGet(this Transform trans)
		{
			return trans.position - MnSpace.trans.position;
		}
		
		public static Vector3 SpacePositionGet(this Rigidbody trans)
		{
			return trans.position - MnSpace.trans.position;
		}
		
		public static void SpacePositionSet(this Transform trans,Vector3 value)
		{
			trans.position = MnSpace.trans.position + value;
		}
		
		public static void SpacePositionMove(this Rigidbody trans,Vector3 value)
		{
			trans.MovePosition(MnSpace.trans.position + value);
		}
	}
}
#endif
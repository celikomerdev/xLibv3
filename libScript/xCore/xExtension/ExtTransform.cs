﻿#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtTransform
	{
		public static void ResetTransform(this Transform trans, bool resetScale = true)
		{
			trans.localPosition = Vector3.zero;
			trans.localEulerAngles = Vector3.zero;
			if (resetScale) trans.localScale = Vector3.one;
		}
		
		public static void SetTransform(this Transform trans,Transform to, bool resetScale = true)
		{
			trans.position = to.position;
			trans.eulerAngles = to.eulerAngles;
			if (resetScale) trans.localScale = to.lossyScale;
		}
		
		#region Math
		public static void Lerp(this Transform trans, Transform from, Transform to, float t,bool scale = false)
		{
			trans.position = Vector3.LerpUnclamped(from.position,to.position,t);
			trans.rotation = Quaternion.LerpUnclamped(from.rotation,to.rotation,t);
			if(scale) trans.localScale = Vector3.Lerp(from.lossyScale,to.lossyScale,t);
		}
		
		public static void LerpTo(this Transform trans, Transform to, float t,bool scale = false)
		{
			trans.position = Vector3.LerpUnclamped(trans.position,to.position,t);
			trans.rotation = Quaternion.LerpUnclamped(trans.rotation,to.rotation,t);
			if(scale) trans.localScale = Vector3.Lerp(trans.lossyScale,to.lossyScale,t);
		}
		
		public static void SlerpTo(this Transform trans, Transform to, float t,bool scale = false)
		{
			trans.position = Vector3.SlerpUnclamped(trans.position,to.position,t);
			trans.rotation = Quaternion.SlerpUnclamped(trans.rotation,to.rotation,t);
			if(scale) trans.localScale = Vector3.Lerp(trans.lossyScale,to.lossyScale,t);
		}
		#endregion
		
		public static void DestroyChildren(this Transform trans)
		{
			int childCount = trans.childCount;
			for (int i = 0; i < childCount; i++)
			{
				GameObject.Destroy(trans.GetChild(i).gameObject);
			}
		}
		
		public static Transform GetChildLast(this Transform trans)
		{
			if(trans.childCount == 0) return null;
			return trans.GetChild(trans.childCount-1);
		}
	}
}
#endif
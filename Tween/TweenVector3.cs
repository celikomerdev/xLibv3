#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenVector3 : Tween
	{
		[SerializeField]private Vector3 from;
		[SerializeField]private Vector3 to;
		[SerializeField]private EventVector3 eventVector3;
		
		override protected void SetRatio(float value)
		{
			eventVector3.Invoke(Vector3.LerpUnclamped(from,to,value));
		}
		
		public Vector3 From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}
		
		public Vector3 To
		{
			get
			{
				return to;
			}
			set
			{
				to = value;
			}
		}
	}
}
#endif
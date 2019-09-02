#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenVector3 : Tween
	{
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.one;
		[SerializeField]private EventVector3 eventVector3 = new EventVector3();
		
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
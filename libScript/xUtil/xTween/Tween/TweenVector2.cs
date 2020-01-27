#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenVector2 : Tween
	{
		[SerializeField]private Vector2 from = Vector2.zero;
		[SerializeField]private Vector2 to = Vector2.one;
		[SerializeField]private EventVector2 eventVector2 = new EventVector2();
		
		protected override void SetRatio(float value)
		{
			eventVector2.Invoke(Vector2.LerpUnclamped(from,to,value));
		}
		
		public Vector2 From
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
		
		public Vector2 To
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
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolTween
{
	public class TweenFloat : Tween
	{
		[SerializeField]private float from;
		[SerializeField]private float to;
		[SerializeField]private EventFloat eventFloat;
		
		override protected void SetRatio(float value)
		{
			eventFloat.Invoke(Mathf.LerpUnclamped(from,to,value));
		}
		
		public float From
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
		
		public float To
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
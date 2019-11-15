#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenFloat : Tween
	{
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		protected override void SetRatio(float value)
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
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolTween
{
	public class TweenFloatLerpInverse : Tween
	{
		[SerializeField]private float min = 0;
		[SerializeField]private float max = 1;
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		override protected void SetRatio(float value)
		{
			eventFloat.Invoke(Mathf.InverseLerp(min,max,value));
		}
		
		public float Min
		{
			get
			{
				return min;
			}
			set
			{
				min = value;
			}
		}
		
		public float Max
		{
			get
			{
				return max;
			}
			set
			{
				max = value;
			}
		}
	}
}
#endif
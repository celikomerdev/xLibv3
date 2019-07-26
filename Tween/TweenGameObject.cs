#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenGameObject : Tween
	{
		public GameObject target;
		public float threshold = 0;
		public bool reverse;
		
		override protected void SetRatio(float value)
		{
			bool result = (value>threshold);
			if(reverse) result = !result;
			
			if(!target) return;
			target.SetActive(result);
		}
	}
}
#endif
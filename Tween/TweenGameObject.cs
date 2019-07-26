#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenGameObject : Tween
	{
		[SerializeField]private GameObject target;
		[SerializeField]private float threshold = 0;
		[SerializeField]private bool reverse;
		
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
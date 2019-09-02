#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenBool : Tween
	{
		private bool result;
		[SerializeField]private float threshold = 0;
		[SerializeField]private EventBool eventBool = new EventBool();
		
		override protected void SetRatio(float value)
		{
			if(result == (value>threshold)) return;
			result = !result;
			
			eventBool.Invoke(result);
		}
	}
}
#endif
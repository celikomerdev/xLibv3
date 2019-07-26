#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolTween
{
	public class TweenBool : Tween
	{
		private bool result;
		[SerializeField]private float threshold = 0;
		[SerializeField]private EventBool eventBool;
		
		override protected void SetRatio(float value)
		{
			if(result == (value>threshold)) return;
			result = !result;
			
			eventBool.Invoke(result);
		}
	}
}
#endif
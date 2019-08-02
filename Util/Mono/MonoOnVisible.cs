#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnVisible : BaseWorkM
	{
		public EventBool eventBool;
		
		private void OnVisible(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnVisible:{0}",value);
			eventBool.Invoke(value);
		}
		
		private void OnBecameVisible()
		{
			OnVisible(true);
		}
		
		private void OnBecameInvisible()
		{
			OnVisible(false);
		}
	}
}
#endif
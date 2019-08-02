#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnApplicationFocus : BaseWorkM
	{
		public EventBool onApplicationFocus;
		
		private void OnApplicationFocus(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnApplicationFocus:{0}",value);
			onApplicationFocus.Invoke(value);
		}
	}
}
#endif
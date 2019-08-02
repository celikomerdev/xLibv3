#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnApplicationPause : BaseWorkM
	{
		public EventBool onApplicationPause;
		
		private void OnApplicationPause(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnApplicationPause:{0}",value);
			onApplicationPause.Invoke(value);
		}
	}
}
#endif
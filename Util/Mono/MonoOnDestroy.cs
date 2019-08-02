#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnDestroy : BaseWorkM
	{
		public EventUnity onDestroy;
		
		private void OnDestroy()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnDestroy");
			onDestroy.Invoke();
		}
	}
}
#endif
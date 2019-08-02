#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoCall : BaseWorkM
	{
		public EventUnity onCall;
		
		[ContextMenu("Call")]
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoCall");
			onCall.Invoke();
		}
	}
}
#endif
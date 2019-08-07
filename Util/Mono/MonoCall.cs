#if xLibv3
using UnityEngine;
using xLib.EventClass;

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
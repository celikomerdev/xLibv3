#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoAwake : BaseActiveM
	{
		public EventUnity awake;
		
		protected override void Awaked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoAwake");
			awake.Invoke();
		}
	}
}
#endif
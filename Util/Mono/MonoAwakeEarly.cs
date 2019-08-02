#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoAwakeEarly : BaseActiveM
	{
		public EventUnity awake;
		
		protected override void Awaked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoAwakeEarly");
			awake.Invoke();
		}
	}
}
#endif
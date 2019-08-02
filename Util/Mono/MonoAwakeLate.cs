#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoAwakeLate : BaseActiveM
	{
		public EventUnity awake;
		
		protected override void Awaked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoAwakeLate");
			awake.Invoke();
		}
	}
}
#endif
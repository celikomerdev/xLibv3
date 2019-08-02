#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoStart : BaseActiveM
	{
		public EventUnity start;
		
		protected override void Started()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoStart");
			start.Invoke();
		}
	}
}
#endif
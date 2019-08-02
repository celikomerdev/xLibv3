#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnDisable : BaseActiveM
	{
		public EventBool onDisable;
		
		protected override void SetActive(bool value)
		{
			value = !value;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnDisable:{0}",value);
			onDisable.Invoke(value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolMono
{
	public class MonoOnEnable : BaseActiveM
	{
		public EventBool onEnable;
		
		protected override void SetActive(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":SetActive:{0}",value);
			onEnable.Invoke(value);
		}
	}
}
#endif
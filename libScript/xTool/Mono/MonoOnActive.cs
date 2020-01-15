#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnActive : BaseActiveM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onEnable")]
		[SerializeField]private EventBool eventActive = new EventBool();
		
		protected override void OnActive(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat($"{this.name}:MonoOnActive:{value}",this);
			eventActive.Invoke(value);
		}
	}
}
#endif
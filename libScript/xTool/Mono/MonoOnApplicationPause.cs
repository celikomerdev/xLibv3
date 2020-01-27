#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnApplicationPause : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onApplicationPause")]
		[SerializeField]private EventBool eventApplicationPause = new EventBool();
		
		private void OnApplicationPause(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat($"{this.name}:MonoOnApplicationPause:{value}",this);
			eventApplicationPause.Invoke(value);
		}
	}
}
#endif
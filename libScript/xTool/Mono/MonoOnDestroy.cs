#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnDestroy : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onDestroy")]
		[SerializeField]private EventUnity eventDestroy = new EventUnity();
		
		private void OnDestroy()
		{
			if(!CanWork) return;
			if(CanDebug) xLogger.LogFormat(this,this.name+":MonoOnDestroy");
			eventDestroy.Invoke();
		}
	}
}
#endif
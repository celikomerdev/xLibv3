#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnCall : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onCall")]
		[SerializeField]private EventUnity eventCall = new EventUnity();
		
		[ContextMenu("Call")]
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat($"{this.name}:MonoOnCall",this);
			eventCall.Invoke();
		}
	}
}
#endif
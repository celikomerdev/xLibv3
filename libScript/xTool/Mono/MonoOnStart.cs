#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnStart : BaseActiveM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("start")]
		[SerializeField]private EventUnity eventStart = new EventUnity();
		
		protected override void Started()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnStart");
			eventStart.Invoke();
		}
	}
}
#endif
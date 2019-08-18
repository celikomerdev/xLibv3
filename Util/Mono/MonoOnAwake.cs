#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnAwake : BaseActiveM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("awake")]
		[SerializeField]private EventUnity eventAwake = new EventUnity();
		
		protected override void Awaked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnAwake");
			eventAwake.Invoke();
		}
	}
}
#endif
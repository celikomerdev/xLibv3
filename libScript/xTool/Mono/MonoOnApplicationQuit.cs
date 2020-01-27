#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnApplicationQuit : BaseWorkM
	{
		[SerializeField]private EventUnity eventApplicationQuit = new EventUnity();
		
		private void OnApplicationQuit()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.Log($"{this.name}:MonoOnApplicationQuit",this);
			eventApplicationQuit.Invoke();
		}
	}
}
#endif
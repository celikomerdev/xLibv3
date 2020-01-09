#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class OnMemoryLow : BaseRegisterM
	{
		protected override bool OnRegister(bool register)
		{
			if(register)
			{
				Application.lowMemory += MemoryLowCallback;
			}
			else
			{
				Application.lowMemory -= MemoryLowCallback;
			}
			return register;
		}
		
		[SerializeField]private EventUnity eventMemoryLow = new EventUnity();
		private void MemoryLowCallback()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:MemoryLowCallback",this);
			eventMemoryLow.Invoke();
		}
	}
}
#endif
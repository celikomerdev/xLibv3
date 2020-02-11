#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class OnMemoryLow : BaseRegisterM
	{
		protected override bool TryRegister(bool register)
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:systemMemorySize:{SystemInfo.systemMemorySize}:totalMemory:{totalMemory}",this);
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
			if(CanDebug) Debug.LogWarning($"{this.name}:MemoryLowCallback:systemMemorySize:{SystemInfo.systemMemorySize}:totalMemory:{totalMemory}",this);
			eventMemoryLow.Invoke();
		}
		
		private static long totalMemory
		{
			get
			{
				return System.GC.GetTotalMemory(false)/(1048576);
			}
		}
	}
}
#endif
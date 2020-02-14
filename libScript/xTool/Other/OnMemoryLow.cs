#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class OnMemoryLow : BaseRegisterM
	{
		protected override bool TryRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:systemMemorySize:{SystemInfo.systemMemorySize}:currentMemory:{currentMemory}",this);
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
		
		[SerializeField]private bool forceFullCollection = false;
		[SerializeField]private EventUnity eventMemoryLow = new EventUnity();
		private void MemoryLowCallback()
		{
			Debug.LogWarning($"{this.name}:MemoryLowCallback:systemMemorySize:{SystemInfo.systemMemorySize}:currentMemory:{currentMemory}",this);
			eventMemoryLow.Invoke();
		}
		
		private long currentMemory
		{
			get
			{
				return System.GC.GetTotalMemory(forceFullCollection)/(1048576);
			}
		}
	}
}
#endif
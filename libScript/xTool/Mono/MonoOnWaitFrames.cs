#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnWaitFrames : BaseWorkM
	{
		[SerializeField]private bool isSingle = true;
		[SerializeField]private bool isDisabled = false;
		[SerializeField]private EventUnity eventWaited = new EventUnity();
		
		public void Wait(int frame)
		{
			if(!CanWork) return;
			
			if(!gameObject.activeInHierarchy) return;
			if(isSingle && processCount>0) return;
			
			if(isDisabled) MnCoroutine.ins.NewCoroutine(eWait(ViewCore.CurrentId,frame),CanDebug);
			else this.NewCoroutine(eWait(ViewCore.CurrentId,frame),CanDebug);
		}
		
		public void CancelAll()
		{
			if(isDisabled) return;
			if(processCount==0) return;
			if(CanDebug) Debug.Log($"{this.name}:CancelAll",this);
			StopAllCoroutines();
			processCount = 0;
		}
		
		private int processCount = 0;
		private IEnumerator eWait(string invokeId,int frame)
		{
			if(CanDebug) Debug.Log($"{this.name}:Wait:{invokeId}:frame:{frame}",this);
			
			processCount++;
			yield return new WaitForFrames(frame);
			processCount--;
			
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = invokeId;
			
			if(CanDebug) Debug.Log($"{this.name}:OnWait:{invokeId}:frame:{frame}",this);
			Profiler.BeginSample($"{this.name}:OnWait:{invokeId}:frame:{frame}",this);
			eventWaited.Invoke();
			Profiler.EndSample();
			
			ViewCore.CurrentId = tempId;
		}
		
		private void OnDisable()
		{
			CancelAll();
		}
		
		[ContextMenu("Call")]
		private void Call()
		{
			if(!CanWork) return;
			eventWaited.Invoke();
		}
	}
}
#endif
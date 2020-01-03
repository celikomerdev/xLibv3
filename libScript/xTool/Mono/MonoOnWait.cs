#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnWait : BaseWorkM
	{
		[SerializeField]private bool isUnscaled = true;
		[SerializeField]private bool isSingle = true;
		[SerializeField]private bool isDisabled = false;
		
		[UnityEngine.Serialization.FormerlySerializedAs("onWait")]
		[SerializeField]private EventUnity eventWaited = new EventUnity();
		
		public void Wait(float time)
		{
			if(!CanWork) return;
			
			if(!gameObject.activeInHierarchy) return;
			if(isSingle && processCount>0) return;
			
			if(isDisabled) MnCoroutine.ins.NewCoroutine(eWait(ViewCore.CurrentId,time));
			else StartCoroutine(eWait(ViewCore.CurrentId,time));
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
		private IEnumerator eWait(string invokeId,float time)
		{
			if(CanDebug) Debug.Log($"{this.name}:Wait:{invokeId}:time:{time}",this);
			
			processCount++;
			if(isUnscaled) yield return new WaitForSecondsRealtime(time);
			else yield return new WaitForSeconds(time);
			processCount--;
			
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = invokeId;
			
			if(CanDebug) Debug.Log($"{this.name}:OnWait:{invokeId}:time:{time}",this);
			Profiler.BeginSample($"{this.name}:OnWait:{invokeId}:time:{time}",this);
			eventWaited.Invoke();
			Profiler.EndSample();
			
			ViewCore.CurrentId = tempId;
		}
		
		private void OnDisable()
		{
			CancelAll();
		}
	}
}
#endif
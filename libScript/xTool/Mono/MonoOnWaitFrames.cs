#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnWaitFrames : BaseWorkM
	{
		[SerializeField]private bool isSingle = true;
		[SerializeField]private bool isDisabled = true;
		[SerializeField]private EventUnity eventWaited = new EventUnity();
		
		public void Wait(int frame)
		{
			if(!CanWork) return;
			
			// if(CanDebug) Debug.Log($"{this.name}:Count:{dictCoroutine.Count}",this);
			if(isSingle && dictCoroutine.Count>0) return;
			
			if(lastCoroutine == ushort.MaxValue) lastCoroutine = 0;
			lastCoroutine++;
			
			Coroutine tempCoroutine = MnCoroutine.ins.NewCoroutine(eWait(ViewCore.CurrentId,frame),CanDebug);
			if(tempCoroutine!=null) dictCoroutine.Add(lastCoroutine,tempCoroutine);
		}
		
		public void CancelAll()
		{
			if(CanDebug) Debug.Log($"{this.name}:CancelAll",this);
			foreach (var item in dictCoroutine)
			{
				MnCoroutine.ins.KillCoroutine(item.Value);
			}
			dictCoroutine = new Dictionary<ushort,Coroutine>();
		}
		
		private ushort lastCoroutine = 0;
		private Dictionary<ushort,Coroutine> dictCoroutine = new Dictionary<ushort,Coroutine>();
		private IEnumerator eWait(string invokeId,int frame)
		{
			ushort cacheCoroutine = lastCoroutine;
			if(CanDebug) Debug.Log($"{this.name}:Wait:{invokeId}:frame:{frame}",this);
			
			yield return new WaitForFrames(frame);
			dictCoroutine.Remove(cacheCoroutine);
			
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
			if(isDisabled) return;
			CancelAll();
		}
		
		[ContextMenu("Call")]
		public void Call()
		{
			if(!CanWork) return;
			eventWaited.Invoke();
		}
	}
}
#endif
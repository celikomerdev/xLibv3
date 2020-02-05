#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnWait : BaseWorkM
	{
		[SerializeField]private bool isUnscaled = true;
		[SerializeField]private bool isSingle = true;
		[SerializeField]private bool isDisabled = true;
		
		[UnityEngine.Serialization.FormerlySerializedAs("onWait")]
		[SerializeField]private EventUnity eventWaited = new EventUnity();
		
		public void Wait(float time)
		{
			if(!CanWork) return;
			
			if(CanDebug) Debug.Log($"{this.name}:Count:{dictCoroutine.Count}",this);
			if(isSingle && dictCoroutine.Count>0) return;
			
			if(lastCoroutine == ushort.MaxValue) lastCoroutine = 0;
			lastCoroutine++;
			
			Coroutine tempCoroutine = MnCoroutine.ins.NewCoroutine(eWait(ViewCore.CurrentId,time),CanDebug);
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
		private IEnumerator eWait(string invokeId,float time)
		{
			ushort cacheCoroutine = lastCoroutine;
			if(CanDebug) Debug.Log($"{this.name}:Wait:{invokeId}:time:{time}:cacheCoroutine:{cacheCoroutine}",this);
			
			if(isUnscaled) yield return new WaitForSecondsRealtime(time);
			else yield return new WaitForSeconds(time);
			dictCoroutine.Remove(cacheCoroutine);
			
			string tempViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = invokeId;
			
			if(CanDebug) Debug.Log($"{this.name}:OnWait:{invokeId}:time:{time}:cacheCoroutine:{cacheCoroutine}",this);
			Profiler.BeginSample($"{this.name}:OnWait:{invokeId}:time:{time}",this);
			eventWaited.Invoke();
			Profiler.EndSample();
			
			ViewCore.CurrentId = tempViewId;
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
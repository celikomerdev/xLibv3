#if xLibv3
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnWait : BaseWorkM
	{
		[SerializeField]private bool isUnscaled = true;
		[SerializeField]private bool isSingle = true;
		
		[UnityEngine.Serialization.FormerlySerializedAs("onWait")]
		[SerializeField]private EventUnity eventWaited = new EventUnity();
		
		public void Wait(float time)
		{
			if(!CanWork) return;
			
			if(!gameObject.activeInHierarchy) return;
			if(isSingle && processCount>0) return;
			StartCoroutine(eWait(time));
		}
		
		public void CancelAll()
		{
			if(processCount==0) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":CancelAll");
			StopAllCoroutines();
			processCount = 0;
		}
		
		private int processCount = 0;
		private IEnumerator eWait(float time)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnWait:{0}",time);
			
			processCount++;
			if(isUnscaled) yield return new WaitForSecondsRealtime(time);
			else yield return new WaitForSeconds(time);
			processCount--;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":OnWait:{0}",time);
			ApplyViewIdWithDebug();
			eventWaited.Invoke();
			ApplyLastIdWithDebug();
		}
		
		private void OnDisable()
		{
			CancelAll();
		}
	}
}
#endif
#if xLibv3
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolMono
{
	public class MonoWait : BaseWorkM
	{
		public bool isUnscaled = true;
		public bool isSingle = false;
		public EventUnity onWait;
		
		public void Wait(float time)
		{
			if(!CanWork) return;
			
			if(!gameObject.activeInHierarchy) return;
			if(isSingle && inWait) return;
			StartCoroutine(eWait(time));
		}
		
		public void CancelAll()
		{
			if(!inWait) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":CancelAll");
			StopAllCoroutines();
			inWait = false;
		}
		
		private bool inWait;
		private IEnumerator eWait(float time)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoWait:{0}",time);
			
			inWait = true;
			if(isUnscaled) yield return new WaitForSecondsRealtime(time);
			else yield return new WaitForSeconds(time);
			
			if(CanDebug) Debug.LogFormat(this,this.name+":OnWait:{0}",time);
			ApplyViewIdWithDebug();
			onWait.Invoke();
			ApplyLastIdWithDebug();
			
			inWait = false;
		}
		
		private void OnDisable()
		{
			CancelAll();
		}
	}
}
#endif
#if xLibv2
using System.Collections;
using UnityEngine;
using xLib.ToolEventClass;

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
			//StartCoroutine(eWait(MnPlayer.CurrentId,time));
			StartCoroutine(eWait(MnPlayer.MyId,time));
		}
		
		public void CancelAll()
		{
			if(!inWait) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":CancelAll");
			StopAllCoroutines();
			inWait = false;
		}
		
		private bool inWait;
		private IEnumerator eWait(string invokeId,float time)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoWait:{0}",time);
			
			inWait = true;
			if(isUnscaled) yield return new WaitForSecondsRealtime(time);
			else yield return new WaitForSeconds(time);
			
			if(CanDebug) Debug.LogFormat(this,this.name+":OnWait:{0}",time);
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = invokeId;
			onWait.Invoke();
			MnPlayer.CurrentId = tempId;
			
			inWait = false;
			yield return null;
		}
		
		private void OnDisable()
		{
			CancelAll();
		}
	}
}
#endif
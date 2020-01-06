﻿#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using xLib.EventClass;

namespace xLib
{
	public class UWRLoad : BaseWorkM
	{
		[SerializeField]private EventUWR eventUWR = new EventUWR();
		
		#region Url
		private ValueBase<string> url = new ValueMulti<string>();
		public string Url
		{
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(url.ValueGet(viewId:ViewCore.CurrentId) == value) return;
				url.ValueSet(value,viewId:ViewCore.CurrentId);
				MnCoroutine.ins.NewCoroutine(eDownload(value,ViewCore.CurrentId),CanDebug);
			}
		}
		#endregion
		
		#region Download
		private IEnumerator eDownload(string url,string viewId)
		{
			UnityWebRequest uwr = UnityWebRequest.Get(url);
			UnityWebRequestAsyncOperation uwrOp = uwr.SendWebRequest();
			
			while (!uwr.isDone)
			{
				if(CanDebug) xLogger.LogFormat(this,this.name+"UWRLoad:{0}",uwrOp.progress);
				yield return new WaitForSecondsRealtime(1f);
			}
			
			if (string.IsNullOrEmpty(uwr.error))
			{
				string tempId = ViewCore.CurrentId;
				ViewCore.CurrentId = viewId;
				eventUWR.Invoke(uwr);
				ViewCore.CurrentId = tempId;
			}
			else
			{
				xLogger.LogExceptionFormat(this,this.name+":Error:{0}:{1}",uwr.error,url);
			}
			
			uwr.Dispose();
			uwr = null;
		}
		#endregion
	}
}
#endif
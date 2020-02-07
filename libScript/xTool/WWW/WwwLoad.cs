﻿#if xLibv3
#if ModWebWWW
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class WwwLoad : BaseWorkM
	{
		[SerializeField]private EventWWW eventWWW = new EventWWW();
		
		#region Url
		private ValueBase<string> url = new ValueMulti<string>();
		public string Url
		{
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(url.ValueGet(viewId:ViewCore.CurrentId) == value) return;
				url.ValueSet(value:value,viewId:ViewCore.CurrentId);
				MnCoroutine.ins.NewCoroutine(eDownload(value,ViewCore.CurrentId),CanDebug);
			}
		}
		#endregion
		
		#region Download
		private IEnumerator eDownload(string url,string viewId)
		{
			WWW www = new WWW(url);
			yield return www;
			
			if (string.IsNullOrEmpty(www.error))
			{
				string tempViewId = ViewCore.CurrentId;
				ViewCore.CurrentId = viewId;
				eventWWW.Invoke(www);
				ViewCore.CurrentId = tempViewId;
			}
			else
			{
				Debug.LogException(new UnityException($"{this.name}:error:{www.error}:url:{url}"),this);
			}
			
			www.Dispose();
			www = null;
		}
		#endregion
	}
}
#endif
#endif
#if xLibv3
#if ModWebWWW
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class WwwFormLoad : BaseWorkM
	{
		[SerializeField]private WwwFormGroup wwwFormGroup = new WwwFormGroup();
		[SerializeField]private EventWWW eventWWW = new EventWWW();
		
		#region Url
		private ValueBase<string> valueUrl = new ValueMulti<string>();
		public string Url
		{
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(valueUrl.ValueGet(viewId:ViewCore.CurrentId) == value) return;
				valueUrl.ValueSet(value:value,viewId:ViewCore.CurrentId);
				MnCoroutine.ins.NewCoroutine(eDownload(ViewCore.CurrentId,value),CanDebug);
			}
		}
		#endregion
		
		#region Download
		private IEnumerator eDownload(string url,string viewId)
		{
			WWW www = new WWW(url,wwwFormGroup.FormData);
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
			yield return new WaitForEndOfFrame();
		}
		#endregion
	}
}
#endif
#endif
#if xLibv3
// #if ModWebWWW
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class WwwLoad : BaseM
	{
		[SerializeField]private EventWWW eventWWW = new EventWWW();
		
		#region Url
		private ValueBase<string> url = new ValueMulti<string>();
		public string Url
		{
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(url.Value == value) return;
				url.Value = value;
				MnCoroutine.NewCoroutine(eDownload(ViewCore.CurrentId,value));
			}
		}
		#endregion
		
		#region Download
		private IEnumerator eDownload(string invokeId,string url)
		{
			WWW www = new WWW(url);
			yield return www;
			
			if (string.IsNullOrEmpty(www.error))
			{
				string tempId = ViewCore.CurrentId;
				ViewCore.CurrentId = invokeId;
				eventWWW.Invoke(www);
				ViewCore.CurrentId = tempId;
			}
			else
			{
				xDebug.LogExceptionFormat(this,this.name+":Error:{0}:{1}",www.error,url);
			}
			
			www.Dispose();
			www = null;
			yield return null;
		}
		#endregion
	}
}
// #endif
#endif
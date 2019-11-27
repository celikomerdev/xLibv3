#if xLibv3
#if ModWebWWW
using System.Collections;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class WwwFormLoad : BaseMainM
	{
		[Header("ISerializableObject")]
		[SerializeField]private Object[] arrayObjectKey = new Object[0];
		[SerializeField]private Object[] arrayObjectName = new Object[0];
		
		[Header("Event")]
		[SerializeField]private EventWWW eventWWW = new EventWWW();
		
		#region Url
		private ValueBase<string> url = new ValueMulti<string>();
		public string Url
		{
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(url.ValueGet(viewId:ViewCore.CurrentId) == value) return;
				url.ValueSet(value,viewId:ViewCore.CurrentId);
				MnCoroutine.ins.NewCoroutine(eDownload(ViewCore.CurrentId,value));
			}
		}
		#endregion
		
		#region Download
		private IEnumerator eDownload(string url,string viewId)
		{
			WWW www = new WWW(url,FormData);
			yield return www;
			
			if (string.IsNullOrEmpty(www.error))
			{
				string tempId = ViewCore.CurrentId;
				ViewCore.CurrentId = viewId;
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
		
		
		private WWWForm FormData
		{
			get
			{
				WWWForm form = new WWWForm();
				
				ISerializableObject[] iSerializableObjectKey = arrayObjectKey.GetGenericsArray<ISerializableObject>();
				for (int i = 0; i < iSerializableObjectKey.Length; i++)
				{
					form.AddField(iSerializableObjectKey[i].Key,(string)iSerializableObjectKey[i].SerializedObjectRaw);
				}
				
				ISerializableObject[] iSerializableObjectName = arrayObjectName.GetGenericsArray<ISerializableObject>();
				for (int i = 0; i < iSerializableObjectName.Length; i++)
				{
					form.AddField(iSerializableObjectName[i].Name,(string)iSerializableObjectName[i].SerializedObjectName);
				}
				
				return form;
			}
		}
	}
}
#endif
#endif
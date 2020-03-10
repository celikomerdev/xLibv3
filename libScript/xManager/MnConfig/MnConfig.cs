#if xLibv3
using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		private bool isLoaded = false;
		[SerializeField]private bool forceLoad = false;
		[SerializeField]private NodeGroup nodeGroup = null;
		[SerializeField]private NodeTextAsset nodeTextAsset = null;
		
		#region Mono
		protected override void Awaked()
		{
			nodeGroup.Load();
			LoadConfig(nodeTextAsset.Value.text);
			if(!isLoaded) LoadConfig(nodeTextAsset.ValueDefault.text);
		}
		#endregion
		
		
		#region LoadConfig
		public static JObject data = new JObject();
		public static Action onLoadConfig = delegate{};
		private void LoadConfig(string json)
		{
			if(string.IsNullOrWhiteSpace(json))
			{
				Debug.LogWarning(new UnityException($"{this.name}:LoadConfig:json:null"),this);
				return;
			}
			if(CanDebug) Debug.Log($"{this.name}:LoadConfig:json.Length:{json.Length}");
			
			JObject newData = null;
			try
			{
				newData = JObject.Parse(json);
			}
			catch (Exception ex)
			{
				Debug.LogException(new UnityException($"{this.name}:LoadConfig:{ex.Message}",ex),this);
				return;
			}
			
			isLoaded = true;
			data = newData;
			onLoadConfig.Invoke();
		}
		#endregion
		
		
		#region UpdateConfig
		[SerializeField]private WwwFormGroup wwwFormGroup = new WwwFormGroup();
		public void UpdateConfig()
		{
			if(CanDebug) Debug.Log($"{this.name}:UpdateConfig");
			MnCoroutine.ins.NewCoroutine(eLoad());
			
			IEnumerator eLoad()
			{
				string url = MnKey.GetValue("MnConfig");
				if(string.IsNullOrEmpty(url)) yield break;
				
				wwwFormGroup.UnityObject = UnityObject;
				WWWForm wwwForm = wwwFormGroup.FormData;
				if(CanDebug) Debug.Log($"{this.name}:wwwFormGroup:Length:{wwwForm.data.Length}:headers:{wwwForm.headers.ToJsonString()}");
				
				UnityWebRequest uwr = null;
				if(wwwForm.data.Length==0) uwr = UnityWebRequest.Get(url);
				else uwr = UnityWebRequest.Post(url,wwwForm);
				
				UnityWebRequestAsyncOperation uwrOp = uwr.SendWebRequest();
				// yield return uwrOp;
				
				float progress = -1f;
				while (!uwrOp.isDone)
				{
					yield return new WaitForSecondsRealtime(2f);
					
					if(progress == uwrOp.progress) continue;
					progress = uwrOp.progress;
					
					if(CanDebug) Debug.Log($"{this.name}:UWRLoad:progress:{uwrOp.progress}");
				}
				
				if (string.IsNullOrEmpty(uwr.error))
				{
					nodeTextAsset.Value = new TextAsset(uwr.downloadHandler.text);
					if(forceLoad) LoadConfig(nodeTextAsset.Value.text);
					nodeGroup.Save();
				}
				else
				{
					Debug.LogException(new UnityException($"{this.name}:eLoad:error:{uwr.error}:url:{url}"),this);
				}
				
				uwr.Dispose();
				uwr = null;
			}
		}
		#endregion
	}
}
#endif
#if xLibv3
using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using xLib.xNode.NodeObject;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		[SerializeField]private bool forceLoad = false;
		[SerializeField]private NodeGroup nodeGroup = null;
		[SerializeField]private NodeTextAsset nodeTextAsset = null;
		
		#region Mono
		protected override void Awaked()
		{
			LoadConfig(nodeTextAsset.Value.text);
			nodeGroup.Load();
			LoadConfig(nodeTextAsset.Value.text);
			MnConfig.onLoadConfig += OnLoadConfig; OnLoadConfig();
		}
		
		protected override void OnDestroyed()
		{
			MnConfig.onLoadConfig -= OnLoadConfig;
		}
		#endregion
		
		
		#region LoadConfig
		public static JObject data = new JObject();
		public static Action onLoadConfig = delegate{};
		private void LoadConfig(string json)
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadConfig:json:{json}");
			
			if(string.IsNullOrWhiteSpace(json))
			{
				xLogger.LogException($"{this.name}:LoadConfig:json:null");
				return;
			}
			
			JObject newData = null;
			try
			{
				newData = JObject.Parse(json);
			}
			catch (Exception ex)
			{
				xLogger.LogException($"{this.name}:LoadConfig:{ex.Message}:json:{json}");
				return;
			}
			
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
				
				WWWForm wwwForm = wwwFormGroup.FormData;
				if(CanDebug) Debug.Log($"{this.name}:wwwFormGroup:Length:{wwwForm.data.Length}:headers:{wwwForm.headers.ToJsonString()}");
				
				UnityWebRequest uwr = null;
				if(wwwForm.data.Length==0) uwr = UnityWebRequest.Get(url);
				else uwr = UnityWebRequest.Post(url,wwwForm);
				
				UnityWebRequestAsyncOperation uwrOp = uwr.SendWebRequest();
				while (!uwr.isDone)
				{
					if(CanDebug) xLogger.LogFormat($"{this.name}:UWRLoad:progress:{uwrOp.progress}");
					yield return new WaitForSecondsRealtime(1f);
				}
				
				if (string.IsNullOrEmpty(uwr.error))
				{
					nodeTextAsset.Value = new TextAsset(uwr.downloadHandler.text);
					if(forceLoad) LoadConfig(nodeTextAsset.Value.text);
					nodeGroup.Save();
				}
				else
				{
					xLogger.LogException($"{this.name}:eLoad:error:{uwr.error}:url:{url}",this);
				}
				
				uwr.Dispose();
				uwr = null;
			}
		}
		#endregion
		
		
		#region Output
		[SerializeField]private ObjectGroup objectGroup = null;
		private void OnLoadConfig()
		{
			objectGroup.Init(true);
			for (int i = 0; i < objectGroup.iSerializableObject.Length; i++)
			{
				objectGroup.iSerializableObject[i].SerializedObjectRaw = data.SelectToken(objectGroup.iSerializableObject[i].Key);
			}
		}
		#endregion
	}
}
#endif
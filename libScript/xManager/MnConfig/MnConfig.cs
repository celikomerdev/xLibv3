#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
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
		public void UpdateConfig()
		{
			if(CanDebug) Debug.Log($"{this.name}:UpdateConfig");
			nodeTextAsset.Value = new TextAsset("deneme");
			nodeGroup.Save();
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
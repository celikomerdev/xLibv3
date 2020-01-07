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
			MnConfig.onLoadConfig += OnLoadConfig;
			LoadConfig();
		}
		
		protected override void OnDestroyed()
		{
			MnConfig.onLoadConfig -= OnLoadConfig;
		}
		#endregion
		
		
		#region LoadConfig
		private void LoadConfig()
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadConfig");
			nodeGroup.Load();
			if(!nodeTextAsset.Value)
			{
				xLogger.LogException($"{this.name}:textAsset:null");
				return;
			}
			LoadConfig(nodeTextAsset.Value.text);
		}
		
		public static JObject data = new JObject();
		public static Action onLoadConfig = delegate{};
		public static void LoadConfig(string json)
		{
			if(string.IsNullOrWhiteSpace(json))
			{
				xLogger.LogException($"MnConfig:UpdateConfig:json:null");
				return;
			}
			
			JObject newData = null;
			try
			{
				newData = JObject.Parse(json);
			}
			catch (Exception ex)
			{
				xLogger.LogException($"MnConfig:UpdateConfig:{ex.Message}:data:{json}");
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
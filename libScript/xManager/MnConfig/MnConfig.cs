#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		public static JObject data = new JObject();
		public static Action onUpdateConfig = delegate{};
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void LoadConfig()
		{
			xDebug.LogTempFormat("MnConfig:LoadConfig");
			TextAsset textAsset = Resources.Load<TextAsset>("ConfigData");
			if(!textAsset)
			{
				xDebug.LogExceptionFormat("MnConfig:textAsset:null");
				return;
			}
			UpdateConfig(textAsset.text);
		}
		
		public static void UpdateConfig(string json)
		{
			// xDebug.LogTempFormat("MnConfig:UpdateConfig:{0}",json);
			if(string.IsNullOrWhiteSpace(json))
			{
				xDebug.LogExceptionFormat("MnConfig:UpdateConfig:json:null");
				return;
			}
			
			JObject newData = null;
			try
			{
				newData = JObject.Parse(json);
			}
			catch (Exception ex)
			{
				xDebug.LogExceptionFormat("MnConfig:UpdateConfig:{0}:{1}",ex,json);
				return;
			}
			
			data = newData;
			onUpdateConfig.Invoke();
		}
		
		protected override void Started()
		{
			MnConfig.onUpdateConfig += OnUpdateConfigData; OnUpdateConfigData();
		}
		
		protected override void OnDestroyed()
		{
			MnConfig.onUpdateConfig -= OnUpdateConfigData;
		}
		
		[SerializeField]private ObjectGroup objectGroup = null;
		private void OnUpdateConfigData()
		{
			objectGroup.Init(true);
			for (int i = 0; i < objectGroup.iSerializableObject.Length; i++)
			{
				objectGroup.iSerializableObject[i].SerializedObjectRaw = data.SelectToken(objectGroup.iSerializableObject[i].Key);
			}
		}
	}
}
#endif
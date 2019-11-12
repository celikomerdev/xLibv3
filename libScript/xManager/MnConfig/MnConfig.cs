#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		public static TextAsset assetConfigData = null;
		public static JObject data = null;
		public static Action onUpdateConfig = delegate{};
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void UpdateConfig()
		{
			Debug.Log("MnConfig");
			assetConfigData = Resources.Load<TextAsset>("ConfigData");
			data = JObject.Parse(assetConfigData.text);
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
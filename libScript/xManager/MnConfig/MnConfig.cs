#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		public static TextAsset assetConfigData;
		public static JObject data = null;
		public static Action onUpdateData;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitStatic()
		{
			Debug.Log("MnConfig");
			assetConfigData = Resources.Load<TextAsset>("ConfigData");
			data = JObject.Parse(assetConfigData.text);
			onUpdateData?.Invoke();
		}
	}
}
#endif
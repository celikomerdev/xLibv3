#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnConfigGroup : BaseWorkM
	{
		#region Mono
		private void Awake()
		{
			MnConfig.onLoadConfig += OnLoadConfig; OnLoadConfig();
		}
		
		private void OnDestroy()
		{
			MnConfig.onLoadConfig -= OnLoadConfig;
		}
		#endregion
		
		[SerializeField]private ObjectGroup objectGroup = null;
		private void OnLoadConfig()
		{
			objectGroup.Init(true);
			for (int i = 0; i < objectGroup.iSerializableObject.Length; i++)
			{
				objectGroup.iSerializableObject[i].SerializedObjectRaw = MnConfig.data.SelectToken(objectGroup.iSerializableObject[i].Key);
			}
		}
	}
}
#endif
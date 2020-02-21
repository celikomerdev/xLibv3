#if xLibv3
using UnityEngine;

namespace xLib
{
	[System.Serializable]public class NodeSetting : IDebug
	{
		public Object UnityObject{get;set;}
		public bool CanDebug{get;set;}
		
		[Header("Key")]
		[SerializeField]private string key = "";
		public string Key
		{
			get
			{
				return key;
			}
			#if UNITY_EDITOR
			set
			{
				if(string.IsNullOrEmpty(value)) return;
				if(key==value) return;
				Debug.Log($"{UnityObject.name}:KeyChange:{key}:{value}",UnityObject);
				key = value;
				
				UnityEditor.EditorUtility.SetDirty(UnityObject);
			}
			#endif
		}
		
		[Header("Multi")]
		[SerializeField]internal bool isMulti = false;
		
		
		private string rpcTarget;
		public string RpcTarget
		{
			get
			{
				return rpcTarget;
			}
			set
			{
				rpcTarget = value;
				if(CanDebug) Debug.Log($"{UnityObject.name}:RpcTarget:{rpcTarget}",UnityObject);
			}
		}
		
		private bool useRpc;
		public bool UseRpc
		{
			get
			{
				if(!ViewCore.inRoom) return false;
				if(!useRpc) return false;
				return useRpc;
			}
			set
			{
				useRpc = value;
				if(CanDebug) Debug.Log($"{UnityObject.name}:UseRpc:{useRpc}",UnityObject);
			}
		}
	}
}
#endif
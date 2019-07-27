#if xLibv3
using UnityEngine;

namespace xLib
{
	[System.Serializable]internal class NodeSetting
	{
		[Header("Debug")]
		internal bool canDebug = false;
		internal Object objDebug = null;
		
		
		[Header("Key")]
		[SerializeField]private string key = "";
		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				#if UNITY_EDITOR
				if(string.IsNullOrEmpty(value)) return;
				if(key==value) return;
				Debug.LogWarningFormat(objDebug,Name+":KeyChange:{0}:{1}",key,value);
				key = value;
				
				UnityEditor.EditorUtility.SetDirty(objDebug);
				#endif
			}
		}
		
		public string Name
		{
			get
			{
				return objDebug.name;
			}
		}
		
		
		[Header("Analytics")]
		[SerializeField]internal AnalyticsType analytics = AnalyticsType.Disabled;
		
		[Header("Multi")]
		[SerializeField]internal bool isMulti = false;
		
		
		private string rpcTarget;
		internal string RpcTarget
		{
			get
			{
				return rpcTarget;
			}
			set
			{
				rpcTarget = value;
				if(canDebug) Debug.LogFormat(objDebug,Name+":RpcTarget:{0}",rpcTarget);
			}
		}
		
		private bool useRpc;
		internal bool UseRpc
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
				if(canDebug) Debug.LogFormat(objDebug,Name+":UseRpc:{0}",useRpc);
			}
		}
	}
	
	internal enum AnalyticsType:int
	{
		Disabled = 0,
		Name = 1,
		Key = 2
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.xNode.NodeObject
{
	public abstract class NodeSave : NodeBase
	{
		[ContextMenu("Save")]
		public void Save()
		{
			MnThread.StartThread(useThread:false,call:delegate
			{
				string value = SerializedObject.ToString();
				if(CanDebug) Debug.LogFormat(this,this.name+":Save:{0}",value);
				xPersistentData.SetString(Key,value,useThread:true);
			});
			
			// TODO enable
			// if(Key!=Name) xPersistentData.DeleteKey(Name);
			// if(PlayerPrefs.HasKey(Key)) PlayerPrefs.DeleteKey(Key);
			// if(PlayerPrefs.HasKey(Name)) PlayerPrefs.DeleteKey(Name);
		}
		
		[ContextMenu("Load")]
		public void Load()
		{
			string value = "";
			
			if(!string.IsNullOrEmpty(value)) value = "Test";
			else if(xPersistentData.HasKey(Key)) value = xPersistentData.GetString(Key);
			else if(xPersistentData.HasKey(Name)) value = xPersistentData.GetString(Name);
			else if(PlayerPrefs.HasKey(Key)) value = PlayerPrefs.GetString(Key);
			else if(PlayerPrefs.HasKey(Name)) value = PlayerPrefs.GetString(Name);
			
			if(CanDebug) Debug.LogFormat(this,this.name+":Load:{0}",value);
			SerializedObject = value;
		}
		
		[ContextMenu("Delete")]
		public void Delete()
		{
			if(!xPersistentData.HasKey(Key)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Delete:{0}",Key);
			xPersistentData.DeleteKey(Key);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("DebugSerializedObjectRaw")]
		private void DebugSerializedObjectRaw()
		{
			Debug.LogFormat(this,this.name+":SerializedObjectRaw:{0}",SerializedObjectRaw.ToString());
		}
		
		[ContextMenu("DebugSerializedObject")]
		private void DebugSerializedObject()
		{
			Debug.LogFormat(this,this.name+":SerializedObject:{0}",SerializedObject.ToString());
		}
		#endif
	}
}
#endif
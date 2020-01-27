#if xLibv3
using UnityEngine;

namespace xLib.xNode.NodeObject
{
	public abstract class MonoSave : MonoBase
	{
		[ContextMenu("Save")]
		public void Save()
		{
			if(CanDebug) Debug.Log($"{this.name}:Save:{Key}",this);
			MnThread.StartThread(useThread:false,call:delegate
			{
				string value = SerializedObject.ToString();
				xPersistentData.SetString(Key,value,useThread:false);
			});
			
			// TODO enable
			// if(Key!=Name) xPersistentData.DeleteKey(Name);
			// if(PlayerPrefs.HasKey(Key)) PlayerPrefs.DeleteKey(Key);
			// if(PlayerPrefs.HasKey(Name)) PlayerPrefs.DeleteKey(Name);
		}
		
		[ContextMenu("Load")]
		public void Load()
		{
			if(CanDebug) Debug.Log($"{this.name}:Load:{Key}",this);
			string value = "";
			
			if(!string.IsNullOrEmpty(value)) value = "Test";
			else if(xPersistentData.HasKey(Key)) value = xPersistentData.GetString(Key);
			else if(xPersistentData.HasKey(Name)) value = xPersistentData.GetString(Name);
			else if(PlayerPrefs.HasKey(Key)) value = PlayerPrefs.GetString(Key);
			else if(PlayerPrefs.HasKey(Name)) value = PlayerPrefs.GetString(Name);
			SerializedObject = value;
		}
		
		[ContextMenu("Delete")]
		public void Delete()
		{
			if(!xPersistentData.HasKey(Key)) return;
			if(CanDebug) Debug.Log($"{this.name}:Delete:{Key}",this);
			xPersistentData.DeleteKey(Key);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("DebugSerializedObjectRaw")]
		private void DebugSerializedObjectRaw()
		{
			Debug.Log($"{this.name}:SerializedObjectRaw:{SerializedObjectRaw}",this);
		}
		
		[ContextMenu("DebugSerializedObject")]
		private void DebugSerializedObject()
		{
			Debug.Log($"{this.name}:SerializedObject:{SerializedObject}",this);
		}
		#endif
	}
}
#endif
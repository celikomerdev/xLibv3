#if xLibv2
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class MnRPC : SingletonM<MnRPC>
	{
		internal static bool inRpc;
		
		protected override void Started()
		{
			SetUseRpc(true);
			InitDictionary();
		}
		
		protected override void OnDestroyed()
		{
			SetUseRpc(false);
		}
		
		private void SetUseRpc(bool value)
		{
			for (int i = 0; i < arraySerializableObject.Length; i++)
			{
				IRpc tempRpc = (IRpc)arraySerializableObject[i];
				tempRpc.UseRpc = true;
			}
		}
		
		[SerializeField]private Object[] arraySerializableObject;
		private Dictionary<string,ISerializableObject> dictSerializableObject = new Dictionary<string,ISerializableObject>();
		private void InitDictionary()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":InitDictionary");
			ISerializableObject[] array = arraySerializableObject.GetGenericsArray<ISerializableObject>();
			
			for (int i = 0; i < array.Length; i++)
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":InitDictionary:{0}",array[i].Key);
				dictSerializableObject.Add(array[i].Key,array[i]);
			}
		}
		
		internal void AllRpc(string senderId,string key,string data)
		{
			
			inRpc = true;
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = senderId;
			
			if(!dictSerializableObject.ContainsKey(key))
			{
				if(CanDebug) Debug.LogWarningFormat(this,this.name+":!ContainsKey:{0}",key);
			}
			else
			{
				ISerializableObject tempSerializableObject = dictSerializableObject[key];
				if(CanDebug) Debug.LogFormat(this,this.name+":NodeValueRpc:{0}:{1}:{2}",senderId,((Object)tempSerializableObject).name,data);
				tempSerializableObject.SerializedObjectRaw = data;
			}
			
			MnPlayer.CurrentId = tempId;
			inRpc = false;
		}
	}
}
#endif
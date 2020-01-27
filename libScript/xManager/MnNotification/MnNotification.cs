#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class MnNotification : SingletonM<MnNotification>
	{
		private bool isInit = false;
		protected override void Inited()
		{
			if(isInit) return;
			isInit = true;
			InitPayload();
		}
		
		public static Action actionNotificationReceive = delegate{};
		public void NotificationReceive()
		{
			StPopupBar.QueueMessage(MnLocalize.GetValue("You Have New Notification"));
			actionNotificationReceive();
		}
		
		public static Action<bool,string> actionNotificationOpen = delegate{};
		public void NotificationOpen(bool useData,string data)
		{
			if(string.IsNullOrWhiteSpace(data)) data = "";
			actionNotificationOpen(useData,data);
			ConsumePayload(useData,data);
		}
		
		#region Tags
		public static Action<Dictionary<string,string>> actionSendTags = delegate{};
		[SerializeField]private UnityEngine.Object[] arrayTag = new UnityEngine.Object[0];
		public void SendTags()
		{
			if(CanDebug) Debug.Log($"{this.name}:SendTags",this);
			Dictionary<string,string> dict = new Dictionary<string,string>();
			ISerializableObject[] array = arrayTag.GetGenericsArray<ISerializableObject>();
			for (int i = 0; i < array.Length; i++)
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":InitDictionary:{0}",array[i].Key,array[i].SerializedObjectRaw.ToString());
				dict.Add(array[i].Key,array[i].SerializedObjectRaw.ToString());
			}
			actionSendTags(dict);
		}
		#endregion
		
		
		#region Payload
		[SerializeField]private UnityEngine.Object[] arrayPayload = new UnityEngine.Object[0];
		private Dictionary<string,ISerializableObject> dictPayload = new Dictionary<string,ISerializableObject>();
		private void InitPayload()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":InitPayload");
			ISerializableObject[] array = arrayPayload.GetGenericsArray<ISerializableObject>();
			for (int i = 0; i < array.Length; i++)
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":InitPayload:{0}",array[i].Key);
				dictPayload.Add(array[i].Key,array[i]);
			}
		}
		
		public void ConsumePayload(bool useData,string data)
		{
			if(CanDebug) Debug.Log($"{this.name}:ConsumePayload:{data}",this);
			if(!useData)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("You Have Already Used This"));
				return;
			}
			
			Dictionary<string,object> dict = new Dictionary<string,object>();
			dict = dict.FromJsonStringSafe(data);
			
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			foreach (KeyValuePair<string,ISerializableObject> pair in dictPayload)
			{
				if(dict.ContainsKey(pair.Key)) pair.Value.SerializedObjectRaw = dict[pair.Key];
			}
			ViewCore.CurrentId = tempId;
		}
		#endregion
	}
}
#endif
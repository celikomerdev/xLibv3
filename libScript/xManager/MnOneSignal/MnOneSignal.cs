#if xLibv3
#if OneSignal
using System.Collections.Generic;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	internal class MnOneSignal : SingletonM<MnOneSignal>
	{
		#region Init
		[SerializeField]private NodeBool isInit;
		private bool inInit;
		public override void Init()
		{
			base.Init();
			if(isInit.Value) return;
			if(inInit) return;
			inInit = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Init");
			
			InitPayload();
			
			string appId = MnKey.GetValue("OneSignal-Id");
			string googleProjectNumber = MnKey.GetValue("Google-Project-Number");
			
			if(CanDebug) OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE,OneSignal.LOG_LEVEL.NONE);
			else OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.NONE,OneSignal.LOG_LEVEL.NONE);
			
			OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
			
			OneSignal.UnityBuilder builder = OneSignal.StartInit(appId,googleProjectNumber);
			builder.notificationReceivedDelegate += OnReceive;
			builder.notificationOpenedDelegate += OnOpen;
			builder.EndInit();
			
			OneSignal.IdsAvailable(IdsAvailable);
			OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
		}
		
		private void OnInit()
		{
			isInit.Value = true;
			inInit = false;
		}
		#endregion
		
		
		#region Callback
		[SerializeField]private NodeString userID;
		[SerializeField]private NodeString pushToken;
		[SerializeField]private NodeString lastNotificationId;
		private void IdsAvailable(string userID, string pushToken)
		{
			this.userID.Value = userID;
			this.pushToken.Value = pushToken;
			OnInit();
		}
		
		private void OnReceive(OSNotification notification)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnReceive:{0}",notification);
			StPopupBar.QueueMessage("you have new notification",true);
		}
		
		private OSNotificationPayload payload;
		private void OnOpen(OSNotificationOpenedResult result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnOpen:{0}",result);
			payload = result.notification.payload;
			TryConsumePayload();
		}
		#endregion
		
		
		#region General
		public void UserDidProvideConsent(bool value)
		{
			OneSignal.UserDidProvideConsent(value);
		}
		
		public void SetExternalUserId(string value)
		{
			if(string.IsNullOrEmpty(value)) return;
			OneSignal.SetExternalUserId(value);
		}
		#endregion
		
		
		#region Tag
		[SerializeField]private Object[] arrayTag;
		public void SendTags()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":SendTags");
			
			Dictionary<string,string> dict = new Dictionary<string,string>();
			ISerializableObject[] array = arrayTag.GetGenericsArray<ISerializableObject>();
			for (int i = 0; i < array.Length; i++)
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":InitDictionary:{0}",array[i].Key,array[i].SerializedObjectRaw.ToString());
				dict.Add(array[i].Key,array[i].SerializedObjectRaw.ToString());
			}
			
			OneSignal.SendTags(dict);
		}
		#endregion
		
		
		#region Payload
		[SerializeField]private Object[] arrayPayload;
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
		
		public void TryConsumePayload()
		{
			if(payload.additionalData == null) return;
			DebugPayload();
			
			if(lastNotificationId.Value == payload.notificationID)
			{
				StPopupBar.QueueMessage("you have already used this",true);
				return;
			}
			lastNotificationId.Value = payload.notificationID;
			
			ConsumePayload();
			OneSignal.ClearOneSignalNotifications();
		}
		
		private void ConsumePayload()
		{
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			
			foreach (var item in dictPayload)
			{
				if(!payload.additionalData.ContainsKey(item.Key)) continue;
				item.Value.SerializedObjectRaw = payload.additionalData[item.Key];
			}
			
			ViewCore.CurrentId = tempId;
		}
		
		private void DebugPayload()
		{
			if(!CanDebug) return;
			foreach (var item in payload.additionalData)
			{
				Debug.LogFormat(this,this.name+":Payload:{0}:{1}",item.Key,item.Value);
			}
		}
		#endregion
	}
}
#else
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	internal class MnOneSignal : SingletonM<MnOneSignal>
	{
		[SerializeField]private NodeBool isInit;
		[SerializeField]private NodeString userID;
		[SerializeField]private NodeString pushToken;
		[SerializeField]private NodeString lastNotificationId;
		
		
		public void UserDidProvideConsent(bool value){}
		public void SetExternalUserId(string value){}
		
		[SerializeField]private Object[] arrayTag;
		public void SendTags(){}
		
		[SerializeField]private Object[] arrayPayload;
		public void TryConsumePayload(){}
	}
}
#endif
#endif
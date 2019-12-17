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
		[SerializeField]private NodeBool isInit = null;
		private bool inInit;
		public override void Init()
		{
			base.Init();
			if(isInit.Value) return;
			if(inInit) return;
			inInit = true;
			
			InitPayload();
			
			string appId = MnKey.GetValue("OneSignal-Id");
			string googleProjectNumber = MnKey.GetValue("Google-Project-Number");
			
			if(CanDebug) OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE,OneSignal.LOG_LEVEL.NONE);
			else OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.NONE,OneSignal.LOG_LEVEL.NONE);
			
			OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
			
			OneSignal.UnityBuilder builder = OneSignal.StartInit(appId,googleProjectNumber);
			builder.notificationReceivedDelegate += OnNotificationReceive;
			builder.notificationOpenedDelegate += OnNotificationOpen;
			builder.inAppMessageClickHandlerDelegate += OnMessageClick;
			builder.EndInit();
			
			OneSignal.permissionObserver += ObserverPermission;
			OneSignal.subscriptionObserver += ObserverSubscription;
			OneSignal.emailSubscriptionObserver += ObserverSubscriptionEmail;
			OneSignal.idsAvailableDelegate += IdsAvailable;
			OneSignal.IdsAvailable();
			
			OSPermissionSubscriptionState pushState = OneSignal.GetPermissionSubscriptionState();
		}
		
		private void OnInit()
		{
			isInit.Value = true;
			inInit = false;
		}
		#endregion
		
		
		#region Callback
		[SerializeField]private NodeString userID = null;
		[SerializeField]private NodeString pushToken = null;
		[SerializeField]private NodeString lastNotificationId = null;
		private void IdsAvailable(string userID, string pushToken)
		{
			this.userID.Value = userID;
			this.pushToken.Value = pushToken;
			OnInit();
		}
		
		private void OnNotificationReceive(OSNotification notification)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnNotificationReceive:{0}",notification);
			StPopupBar.QueueMessage(MnLocalize.GetValue("You Have New Notification"));
		}
		
		private OSNotificationPayload payload;
		private void OnNotificationOpen(OSNotificationOpenedResult result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnNotificationOpen:{0}",result);
			payload = result.notification.payload;
			TryConsumePayload();
		}
		
		private void OnMessageClick(OSInAppMessageAction result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnMessageClick:{0}",result);
		}
		
		private void ObserverPermission(OSPermissionStateChanges result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ObserverPermission:{0}",result);
		}
		
		private void ObserverSubscription(OSSubscriptionStateChanges result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ObserverSubscription:{0}",result);
		}
		
		private void ObserverSubscriptionEmail(OSEmailSubscriptionStateChanges result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ObserverSubscriptionEmail:{0}",result);
		}
		#endregion
		
		
		#region General
		public void UserDidProvideConsent(bool value)
		{
			if(inInit || isInit.Value) Debug.LogWarningFormat(this,this.name+"CallBeforeInit");
			OneSignal.UserDidProvideConsent(value);
		}
		
		public void SetRequiresUserPrivacyConsent(bool value)
		{
			if(inInit || isInit.Value) Debug.LogWarningFormat(this,this.name+"CallBeforeInit");
			if(true) UserDidProvideConsent(true);
			OneSignal.SetRequiresUserPrivacyConsent(value);
		}
		
		public void SetExternalUserId(string value)
		{
			if(string.IsNullOrWhiteSpace(value)) return;
			OneSignal.SetExternalUserId(value);
		}
		
		public void PauseInAppMessages(bool value)
		{
			OneSignal.PauseInAppMessages(value);
		}
		#endregion
		
		
		#region Tag
		[SerializeField]private Object[] arrayTag = new Object[0];
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
		[SerializeField]private Object[] arrayPayload = new Object[0];
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
				StPopupBar.QueueMessage(MnLocalize.GetValue("You Have Already Used This"));
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
		#pragma warning disable
		[SerializeField]private NodeBool isInit = null;
		[SerializeField]private NodeString userID = null;
		[SerializeField]private NodeString pushToken = null;
		[SerializeField]private NodeString lastNotificationId = null;
		
		public void UserDidProvideConsent(bool value){}
		public void SetRequiresUserPrivacyConsent(bool value){}
		public void SetExternalUserId(string value){}
		public void PauseInAppMessages(bool value){}
		
		[SerializeField]private Object[] arrayTag = new Object[0];
		public void SendTags(){}
		
		[SerializeField]private Object[] arrayPayload = new Object[0];
		public void TryConsumePayload(){}
		#pragma warning restore
	}
}
#endif
#endif
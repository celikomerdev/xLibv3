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
		protected override void Inited()
		{
			if(isInit.Value) return;
			if(inInit) return;
			inInit = true;
			
			string appId = MnKey.GetValue("OneSignal_Id");
			string googleProjectNumber = MnKey.GetValue("Google_Project_Number");
			
			if(CanDebug) OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE,OneSignal.LOG_LEVEL.NONE);
			else OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.WARN,OneSignal.LOG_LEVEL.NONE);
			
			OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
			
			OneSignal.UnityBuilder builder = OneSignal.StartInit(appId,googleProjectNumber);
			builder.notificationReceivedDelegate += OnNotificationReceive;
			builder.notificationOpenedDelegate += OnNotificationOpen;
			builder.inAppMessageClickHandlerDelegate += OnMessageClick;
			builder.EndInit();
			
			OneSignal.permissionObserver += ObserverPermission;
			OneSignal.subscriptionObserver += ObserverSubscription;
			OneSignal.emailSubscriptionObserver += ObserverSubscriptionEmail;
			OneSignal.IdsAvailable(IdsAvailable);
			
			OSPermissionSubscriptionState pushState = OneSignal.GetPermissionSubscriptionState();
		}
		
		private void OnInit()
		{
			isInit.Value = true;
			inInit = false;
		}
		
		protected override void OnEnabled()
		{
			MnNotification.actionSendTags += SendTags;
		}
		
		protected override void OnDisabled()
		{
			MnNotification.actionSendTags -= SendTags;
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
			if(CanDebug) Debug.Log($"{this.name}:OnNotificationReceive:{notification}",this);
			MnNotification.ins.NotificationReceive();
		}
		
		private void OnNotificationOpen(OSNotificationOpenedResult result)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnNotificationOpen:{result}",this);
			TryConsumePayload(result.notification.payload);
		}
		
		private void OnMessageClick(OSInAppMessageAction result)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnMessageClick:{result}",this);
		}
		
		private void ObserverPermission(OSPermissionStateChanges result)
		{
			if(CanDebug) Debug.Log($"{this.name}:ObserverPermission:{result}",this);
		}
		
		private void ObserverSubscription(OSSubscriptionStateChanges result)
		{
			if(CanDebug) Debug.Log($"{this.name}:ObserverSubscription:{result}",this);
		}
		
		private void ObserverSubscriptionEmail(OSEmailSubscriptionStateChanges result)
		{
			if(CanDebug) Debug.Log($"{this.name}:ObserverSubscriptionEmail:{result}",this);
		}
		#endregion
		
		
		#region General
		public void PauseInAppMessages(bool value)
		{
			OneSignal.PauseInAppMessages(value);
		}
		
		public void SetExternalUserId(string value)
		{
			if(string.IsNullOrWhiteSpace(value)) return;
			OneSignal.SetExternalUserId(value);
		}
		
		public void SetRequiresUserPrivacyConsent(bool value)
		{
			OneSignal.SetRequiresUserPrivacyConsent(value);
		}
		
		public void UserDidProvideConsent(bool value)
		{
			OneSignal.UserDidProvideConsent(value);
		}
		#endregion
		
		#region Tag
		private void SendTags(Dictionary<string,string> dict)
		{
			if(!isInit.Value) return;
			OneSignal.SendTags(dict);
		}
		#endregion
		
		#region Payload
		private void TryConsumePayload(OSNotificationPayload payload)
		{
			bool useData = (lastNotificationId.Value != payload.notificationID);
			lastNotificationId.Value = payload.notificationID;
			
			MnNotification.ins.NotificationOpen(useData,payload.additionalData.ToJsonString());
			OneSignal.ClearOneSignalNotifications();
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
		
		public void PauseInAppMessages(bool value){}
		public void SetExternalUserId(string value){}
		public void SetRequiresUserPrivacyConsent(bool value){}
		public void UserDidProvideConsent(bool value){}
		#pragma warning restore
	}
}
#endif
#endif
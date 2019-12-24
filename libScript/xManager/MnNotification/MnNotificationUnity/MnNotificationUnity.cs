#if xLibv3
using UnityEngine;
using System;
using xLib.xNode.NodeObject;

#if UNITY_IOS
using Unity.Notifications.iOS;
#elif UNITY_ANDROID
using Unity.Notifications.Android;
#endif


namespace xLib
{
	public class MnNotificationUnity : SingletonM<MnNotificationUnity>
	{
		[SerializeField]private NodeString lastNotificationId = null;
		
		[Header("Android")]
		[SerializeField]private string channelId = "Unity Channel ID";
		[SerializeField]private string channelName = "Unity Channel Name";
		[SerializeField]private string channelDescription = "Unity Channel Description";
		[SerializeField]private Importance channelImportance = Importance.Default;
		
		protected override void Started()
		{
			Init();
		}
		
		private static bool isInit = false;
		protected override void Inited()
		{
			if(isInit) return;
			InitPlatform();
			isInit = true;
		}
		
		
		#if UNITY_ANDROID
		private static AndroidNotificationChannel channel;
		private void InitPlatform()
		{
			channel = new AndroidNotificationChannel();
			channel.Id = channelId;
			channel.Name = channelName;
			channel.Description = channelDescription;
			channel.Importance = channelImportance;
			AndroidNotificationCenter.RegisterNotificationChannel(channel);
			AndroidNotificationCenter.OnNotificationReceived += receiveData =>{NotificationReceive();};
			
			AndroidNotificationIntentData lastData = AndroidNotificationCenter.GetLastNotificationIntent();
			if(lastData!=null) NotificationOpen(lastData.Id,lastData.Notification.IntentData);
		}
		#endif
		
		
		#if UNITY_IOS
		private void InitPlatform()
		{
			iOSNotificationCenter.OnRemoteNotificationReceived += receiveData =>{NotificationReceive();};
			iOSNotification lastData = iOSNotificationCenter.GetLastRespondedNotification();
			if(lastData!=null) NotificationOpen(lastData.Identifier,lastData.Data);
		}
		#endif
		
		
		public static int CreateNotification(DateTime dateTime, string title, string message, string intentData = "")
		{
			int id = UnityEngine.Random.Range(1,9999999);
			
			#if UNITY_ANDROID
			AndroidNotification notification = new AndroidNotification();
			notification.Title = title;
			notification.Text = message;
			notification.FireTime = dateTime;
			if(!string.IsNullOrWhiteSpace(intentData)) notification.IntentData = intentData;
			id = AndroidNotificationCenter.SendNotification(notification,channel.Name);
			#endif
			
			
			#if UNITY_IOS
			iOSNotification notification = new iOSNotification();
			notification.Title = title;
			notification.Body = message;
			iOSNotificationTimeIntervalTrigger trigger = new iOSNotificationTimeIntervalTrigger
			{
				TimeInterval = dateTime.Subtract(DateTime.Now),
				Repeats = false
			};
			notification.Trigger = trigger;
			notification.Identifier = id.ToString();
			iOSNotificationCenter.ScheduleNotification(notification);
			#endif
			
			return id;
		}
		
		private void NotificationReceive()
		{
			MnNotification.NotificationReceive();
		}
		
		private void NotificationOpen(int id,string data)
		{
			bool useData = (lastNotificationId.Value != id.ToString());
			lastNotificationId.Value = id.ToString();
			MnNotification.NotificationOpen(useData,data);
		}
		
		public void CancelNotification(int id)
		{
			#if UNITY_ANDROID
			AndroidNotificationCenter.CancelNotification(id);
			#endif
			
			#if UNITY_IOS
			iOSNotificationCenter.RemoveScheduledNotification(id.ToString());
			#endif
		}
		
		public void CancelAllNotifications()
		{
			#if UNITY_ANDROID
			AndroidNotificationCenter.CancelAllNotifications();
			#endif
			
			#if UNITY_IOS
			iOSNotificationCenter.RemoveAllScheduledNotifications();
			iOSNotificationCenter.RemoveAllDeliveredNotifications();
			#endif
		}
	}
}
#endif
﻿#if xLibv3
#if NotificationUnity
using UnityEngine;
using System;
using xLib.xValueClass;

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
#if UNITY_ANDROID
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
		
		
		private static bool isInit = false;
		protected override void Inited()
		{
			if(isInit) return;
			InitPlatform();
			isInit = true;
			
			CheckLast();
			CancelAllNotifications();
			if(CanDebug) CreateNotification(dateTime:SafeTime.NowLocal.AddSeconds(30),title:"MnNotificationUnity",message:"Test",data:"Test");
		}
		
		private void OnApplicationFocus(bool value)
		{
			if(!value) return;
			if(!isInit) return;
			CheckLast();
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
			AndroidNotificationCenter.OnNotificationReceived += receiveNotification =>{NotificationReceive();};
		}
		
		private void CheckLast()
		{
			AndroidNotificationIntentData lastNotification = AndroidNotificationCenter.GetLastNotificationIntent();
			if(lastNotification!=null) NotificationOpen(lastNotification.Id,lastNotification.Notification.IntentData);
		}
		#endif
		
		
		#if UNITY_IOS
		private void InitPlatform()
		{
			iOSNotificationCenter.OnRemoteNotificationReceived += receiveNotification =>{NotificationReceive();};
		}
		
		private void CheckLast()
		{
			iOSNotification lastNotification = iOSNotificationCenter.GetLastRespondedNotification();
			if(lastNotification!=null) NotificationOpen(lastNotification.Identifier,lastNotification.Data);
		}
		#endif
		
		
		public int CreateNotification(DateTime dateTime, string title, string message, string data = "")
		{
			if(CanDebug) Debug.Log($"{this.name}:CreateNotification:{title}:{message}:{dateTime.ToString()}:{data}",this);
			int id = UnityEngine.Random.Range(1,9999999);
			
			#if UNITY_ANDROID
			AndroidNotification notification = new AndroidNotification();
			notification.Title = title;
			notification.Text = message;
			notification.FireTime = dateTime;
			notification.SmallIcon = "icon_small";
			notification.LargeIcon = "icon_large";
			if(!string.IsNullOrWhiteSpace(data)) notification.IntentData = data;
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
			if(!string.IsNullOrWhiteSpace(data)) notification.Data = data;
			iOSNotificationCenter.ScheduleNotification(notification);
			#endif
			
			return id;
		}
		
		private void NotificationReceive()
		{
			if(CanDebug) Debug.Log($"{this.name}:NotificationReceive",this);
			MnNotification.NotificationReceive();
		}
		
		private void NotificationOpen(int id,string data)
		{
			if(CanDebug) Debug.Log($"{this.name}:NotificationOpen:{id}:{data}",this);
			bool useData = (lastNotificationId.Value != id.ToString());
			lastNotificationId.Value = id.ToString();
			if(!useData) return;
			MnNotification.ins.NotificationOpen(true,data);
		}
		
		public void CancelNotification(int id)
		{
			if(CanDebug) Debug.Log($"{this.name}:CancelNotification:{id}",this);
			
			#if UNITY_ANDROID
			AndroidNotificationCenter.CancelNotification(id);
			#endif
			
			#if UNITY_IOS
			iOSNotificationCenter.RemoveScheduledNotification(id.ToString());
			#endif
		}
		
		public void CancelAllNotifications()
		{
			if(CanDebug) Debug.Log($"{this.name}:CancelAllNotifications",this);
			
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
#else
using UnityEngine;
using System;
using xLib.xValueClass;

namespace xLib
{
	public class MnNotificationUnity : SingletonM<MnNotificationUnity>
	{
		#pragma warning disable
		[SerializeField]private NodeString lastNotificationId = null;
		
		[Header("Android")]
		[SerializeField]private string channelId = "Unity Channel ID";
		[SerializeField]private string channelName = "Unity Channel Name";
		[SerializeField]private string channelDescription = "Unity Channel Description";
		[SerializeField]private int channelImportance = 4;
		#pragma warning restore
		
		public int CreateNotification(DateTime dateTime, string title, string message, string data = "")
		{
			if(CanDebug) Debug.Log($"{this.name}:CreateNotification:{title}:{message}:{dateTime.ToLongDateString()}:{dateTime.ToLongTimeString()}:{data}",this);
			int id = UnityEngine.Random.Range(1,9999999);
			return id;
		}
		
		public void CancelNotification(int id)
		{
			if(CanDebug) Debug.Log($"{this.name}:CancelNotification:{id}",this);
		}
		
		public void CancelAllNotifications()
		{
			if(CanDebug) Debug.Log($"{this.name}:CancelAllNotifications",this);
		}
	}
}
#endif
#endif
﻿#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class StAdvert
	{
		#region CanDebug
		private static bool trackingEnabled = false;
		internal static bool TrackingEnabled
		{
			get
			{
				return trackingEnabled;
			}
			set
			{
				if(trackingEnabled == value) return;
				trackingEnabled = value;
				xLogger.Log($"StAdvert:trackingEnabled:{trackingEnabled}");
			}
		}
		
		private static string advertisingID = "";
		internal static string AdvertisingID
		{
			get
			{
				return advertisingID;
			}
			set
			{
				if(advertisingID == value) return;
				advertisingID = value;
				xLogger.Log($"StAdvert:advertisingID:{advertisingID}");
				
				if(string.IsNullOrEmpty(advertisingID)) TestDeviceID = "";
				else TestDeviceID = advertisingID.HashMD5UTF8();
			}
		}
		
		private static string testDeviceID = "";
		internal static string TestDeviceID
		{
			get
			{
				return testDeviceID;
			}
			set
			{
				if(Application.platform == RuntimePlatform.Android) value = value.ToUpper();
				
				if(testDeviceID == value) return;
				testDeviceID = value;
				xLogger.Log($"StAdvert:testDeviceID:{testDeviceID}");
			}
		}
		#endregion
		
		internal static void Init()
		{
			DeviceAdvertID.Init();
		}
	}
	
	
	public static class DeviceAdvertID
	{
		#if CanDebug
		internal static void Init()
		{
			CallAdvertisingIdentifierNative();
			CallAdvertisingIdentifierUnity();
		}
		
		private static void CallAdvertisingIdentifierNative()
		{
			xLogger.Log($"DeviceAdvertID:CallAdvertisingIdentifierNative");
			
			string value = "";
			
			#if UNITY_EDITOR
			value = "";
			#elif UNITY_ANDROID
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
			AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
			string name = secure.GetStatic<string>("ANDROID_ID");
			string androidId = secure.CallStatic<string>("getString", contentResolver, name);
			value = androidId;
			#elif UNITY_IOS
			value = UnityEngine.iOS.Device.advertisingIdentifier;
			#endif
			
			StAdvert.AdvertisingID = value;
		}
		
		private static void CallAdvertisingIdentifierUnity()
		{
			xLogger.Log($"DeviceAdvertID:CallAdvertisingIdentifierUnity");
			Application.RequestAdvertisingIdentifierAsync(CallbackAdvertisingIdentifierUnity);
		}
		
		private static void CallbackAdvertisingIdentifierUnity(string advertisingId, bool trackingEnabled, string errorMsg)
		{
			xLogger.Log($"DeviceAdvertID:CallbackAdvertisingIdentifierUnity");
			
			if(!string.IsNullOrEmpty(errorMsg))
			{
				Debug.LogException(new UnityException($"DeviceAdvertID:errorMsg:{errorMsg}"));
				StAdvert.TrackingEnabled = false;
				StAdvert.AdvertisingID = "";
				return;
			}
			
			StAdvert.TrackingEnabled = trackingEnabled;
			StAdvert.AdvertisingID = advertisingId;
		}
		#else
		internal static void Init()
		{
		}
		#endif
	}
}
#endif
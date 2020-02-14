#if xLibv3
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
				Debug.Log($"StAdvert:trackingEnabled:{trackingEnabled}:change:{value}");
				trackingEnabled = value;
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
				Debug.Log($"StAdvert:advertisingID:{advertisingID}:change:{value}");
				advertisingID = value;
				
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
				Debug.Log($"StAdvert:testDeviceID:{testDeviceID}:change:{value}");
				testDeviceID = value;
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
			string value = "";
			
			#if UNITY_EDITOR
			if(Application.isEditor) return;
			#endif
			
			#if UNITY_ANDROID
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
			AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
			string name = secure.GetStatic<string>("ANDROID_ID");
			string androidId = secure.CallStatic<string>("getString", contentResolver, name);
			value = androidId;
			#endif
			
			#if UNITY_IOS
			value = UnityEngine.iOS.Device.advertisingIdentifier;
			#endif
			
			if(string.IsNullOrWhiteSpace(value)) return;
			StAdvert.AdvertisingID = value;
			StAdvert.TrackingEnabled = true;
		}
		
		private static void CallAdvertisingIdentifierUnity()
		{
			Application.RequestAdvertisingIdentifierAsync(delegate(string advertisingId, bool trackingEnabled, string errorMsg)
			{
				if(!string.IsNullOrEmpty(errorMsg))
				{
					Debug.LogException(new UnityException($"DeviceAdvertID:errorMsg:{errorMsg}"));
					return;
				}
				StAdvert.AdvertisingID = advertisingId;
				StAdvert.TrackingEnabled = trackingEnabled;
			});
		}
		#else
		internal static void Init(){}
		#endif
	}
}
#endif
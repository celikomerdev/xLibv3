#if xLibv3
using System.IO;
using UnityEngine;

namespace xLib
{
	public static class xApp
	{
		static xApp()
		{
			Debug.Log($"xApp:Reset");
			// xApp.FillAppVersion();
		}
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		public static void BeforeSplashScreen()
		{
			Debug.Log($"xApp:BeforeSplashScreen");
			persistentDataPath = Application.persistentDataPath;
		}
		
		#region Version
		private static int m_app_version = 0;
		public static int app_version
		{
			get
			{
				return m_app_version;
			}
			private set
			{
				m_app_version = value;
			}
		}
		
		public static void FillAppVersion()
		{
			#if UNITY_EDITOR
			int bundleVersionCode = UnityEditor.PlayerSettings.Android.bundleVersionCode;
			if(app_version == bundleVersionCode) return;
			app_version = bundleVersionCode;
			Debug.Log($"app_version:{app_version}");
			#endif
		}
		#endregion
		
		
		#region Platform
		public static RuntimePlatform platformTarget
		{
			get
			{
				#if UNITY_ANDROID
				return RuntimePlatform.Android;
				#elif UNITY_IOS
				return RuntimePlatform.IPhonePlayer;
				#else
				return Application.platform;
				#endif
			}
		}
		#endregion
		
		
		#region xPath
		public static string xPath(string file)
		{
			string path = xPersistentDataPath+file;
			CreateDirectory(path);
			return path;
		}
		
		public static string customFolder = "/xProject/";
		private static string persistentDataPath = "";
		private static string xPersistentDataPath
		{
			get
			{
				return persistentDataPath+customFolder;
			}
		}
		
		private static void CreateDirectory(string path)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
			catch (System.Exception ex)
			{
				xLogger.LogException($"xApp.CreateDirectory:{ex.Message}");
			}
		}
		#endregion
	}
}
#endif
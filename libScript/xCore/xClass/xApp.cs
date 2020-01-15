#if xLibv3
using System.IO;
using UnityEngine;

namespace xLib
{
	public static class xApp
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void FillApp()
		{
			Debug.LogWarning($"xApp:FillApp");
			persistentDataPath = Application.persistentDataPath;
		}
		
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
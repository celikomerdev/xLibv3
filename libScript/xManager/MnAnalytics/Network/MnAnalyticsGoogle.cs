#if xLibv3
#if AnalyticsGoogle
using System.Collections;
using UnityEngine;
using xLib.AnalyticsGoogle;

namespace xLib.xAnalytics
{
	internal class MnAnalyticsGoogle : SingletonM<MnAnalyticsGoogle>
	{
		private static bool isInit;
		[SerializeField]internal bool sendLaunchEvent=true;
		
		[SerializeField]private GoogleAnalyticsPlatform tracker;
		[SerializeField]private GoogleAnalyticsVx logger;
		
		#region Init
		protected override void Awaked()
		{
			SetApp();
			SetVersion();
			SetTrackingCode();
		}
		
		protected override void OnDisabled()
		{
			tracker.endSessionOnNextHit = true;
			LogScreen("end");
			// LogEvent("Session","End","",MnTime.ins.tickUnscaledStart.Value.ToString("F0"));
		}
		
		public override void Init()
		{
			base.Init();
			if(isInit) return;
			
			tracker.SetTrackerVal(Fields.DEVELOPER_ID,"GbOCSs");
			tracker.InitializeTracker();
			
			isInit = true;
			LogLaunch();
		}
		
		private void SetApp()
		{
			tracker.bundleIdentifier = Application.identifier;
			tracker.appName = Application.productName;
		}
		
		private void SetVersion()
		{
			string appBundleId = MnKey.GetValue("App_NameShort");
			tracker.appVersion = string.Format("{0}-{1}-{2}",appBundleId,Application.version,Application.platform);
		}
		
		private void SetTrackingCode()
		{
			string trackingCode = "";
			
			#if CanDebug
			trackingCode = MnKey.GetValue("Google_Analytics_Id_Editor");
			#else
			trackingCode = MnKey.GetValue("Google_Analytics_Id");
			#endif
			
			tracker.trackingCode = trackingCode;
			tracker.trackingCodeSet = !string.IsNullOrEmpty(trackingCode);
		}
		
		public void SetClientID(string value)
		{
			tracker.clientId = value;
		}
		
		public void SetUserID(string value)
		{
			// tracker.SetTrackerVal("&dimension1",value);
		}
		#endregion
		
		
		#region Log
		private void LogLaunch()
		{
			if(!sendLaunchEvent) return;
			LogScreen("start");
			LogEvent("Google Analytics","Auto Instrumentation","Game Launch","0");
		}
		
		public static void LogScreen(string value)
		{
			if(isInit) ins.logger.LogScreen(value);
		}
		
		public static void LogEvent(string category,string action,string label,string value)
		{
			if(isInit) ins.logger.LogEvent(category,action,label,value);
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib.xAnalytics
{
	internal class MnAnalyticsGoogle : SingletonM<MnAnalyticsGoogle>
	{
		[SerializeField]internal bool sendLaunchEvent=true;
		
		public void SetClientID(string value){}
		public void SetUserID(string value){}
		public static void LogScreen(string value){}
		public static void LogEvent(string category,string action,string label,string value){}
	}
}
#endif
#endif
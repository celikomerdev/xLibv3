#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class MnIronSource : BaseWorkM
	{
		[SerializeField]private string key = "";
		[SerializeField]private string[] adUnits = new string[0];
		
		#region Mono
		private void Awake()
		{
			if(CanDebug) Debug.Log($"{this.name}:Awake",this);
			IronSource.Agent.setAdaptersDebug(CanDebug);
			
			MnThread.StartThread(iDebug:this,useThread:false,priority:1,call:delegate
			{
				IronSource.Agent.shouldTrackNetworkState(true);
				FillUser();
				
				IronSource.Agent.init(MnKey.GetValue(key),adUnits);
				if(CanDebug) IronSource.Agent.validateIntegration();
				if(CanDebug) Debug.Log("MnIronSource:isInit:true",this);
				TryRegister(true);
			});
		}
		
		private void OnDestroy()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnDestroy",this);
			TryRegister(false);
		}
		
		#region TryRegister
		private static bool TryRegister(bool value)
		{
			if (value)
			{
				IronSourceEvents.onBannerAdLoadFailedEvent += onBannerAdLoadFailedEvent;
				IronSourceEvents.onBannerAdLoadedEvent += onBannerAdLoadedEvent;
			}
			else
			{
				IronSourceEvents.onBannerAdLoadFailedEvent -= onBannerAdLoadFailedEvent;
				IronSourceEvents.onBannerAdLoadedEvent -= onBannerAdLoadedEvent;
			}
			return value;
		}
		#endregion
		
		private void OnApplicationPause(bool value)
		{
			IronSource.Agent.onApplicationPause(value);
		}
		#endregion
		
		
		#region Init
		private static void FillUser()
		{
			IronSource.Agent.setConsent(MnAdvert.ins.privacyAccepted);
			// IronSource.Agent.setUserId(SystemInfo.deviceUniqueIdentifier);
			
			// if(MnAdvert.ins.age>0) IronSource.Agent.setAge(MnAdvert.ins.age);
			// if(!string.IsNullOrEmpty(MnAdvert.ins.gender)) IronSource.Agent.setGender(MnAdvert.ins.gender);
			
			// IronSourceSegment mIronSegment = new IronSourceSegment();
			// if(MnAdvert.ins.isPaying) mIronSegment.isPaying = 1;
		}
		#endregion
		
		
		#region Banner
		public static bool hasBanner = false;
		private static void onBannerAdLoadFailedEvent(IronSourceError error)
		{
			hasBanner = false;
		}
		
		private static void onBannerAdLoadedEvent()
		{
			hasBanner = true;
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class MnIronSource : BaseWorkM
	{
		[SerializeField]private string key = "";
		[SerializeField]private string[] adUnits = new string[0];
	}
}
#endif
#endif
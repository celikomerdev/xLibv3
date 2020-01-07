#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class MnIronSource : BaseWorkM
	{
		[SerializeField]private string key = "";
		
		#region Mono
		private void Awake()
		{
			if(CanDebug) Debug.Log($"{this.name}:Awake",this);
			Init();
			Register(true);
		}
		
		private void OnDestroy()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnDestroy",this);
			Register(false);
		}
		
		#region Register
		private bool Register(bool value)
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
		private void Init()
		{
			IronSource.Agent.setAdaptersDebug(CanDebug);
			IronSource.Agent.shouldTrackNetworkState(true);
			FillUser();
			
			IronSource.Agent.init(MnKey.GetValue(key),IronSourceAdUnits.REWARDED_VIDEO,IronSourceAdUnits.INTERSTITIAL,IronSourceAdUnits.OFFERWALL,IronSourceAdUnits.BANNER);
			if(CanDebug) IronSource.Agent.validateIntegration();
		}
		
		private void FillUser()
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
		private void onBannerAdLoadFailedEvent(IronSourceError error)
		{
			hasBanner = false;
		}
		
		private void onBannerAdLoadedEvent()
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
	}
}
#endif
#endif
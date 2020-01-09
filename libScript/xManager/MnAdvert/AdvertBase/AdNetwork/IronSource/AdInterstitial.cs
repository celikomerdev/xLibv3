#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdInterstitial : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{register}",this);
			
			if (register)
			{
				IronSourceEvents.onInterstitialAdLoadFailedEvent += onInterstitialAdLoadFailedEvent;
				IronSourceEvents.onInterstitialAdReadyEvent += onInterstitialAdReadyEvent;
				
				IronSourceEvents.onInterstitialAdShowFailedEvent += onInterstitialAdShowFailedEvent;
				IronSourceEvents.onInterstitialAdShowSucceededEvent += onInterstitialAdShowSucceededEvent;
				
				IronSourceEvents.onInterstitialAdClickedEvent += onInterstitialAdClickedEvent;
				IronSourceEvents.onInterstitialAdOpenedEvent += onInterstitialAdOpenedEvent;
				
				IronSourceEvents.onInterstitialAdRewardedEvent += onInterstitialAdRewardedEvent;
				IronSourceEvents.onInterstitialAdClosedEvent += onInterstitialAdClosedEvent;
				
				OnRegisterBase();
				if(IronSource.Agent.isInterstitialReady()) onInterstitialAdReadyEvent();
			}
			else
			{
				IronSourceEvents.onInterstitialAdLoadFailedEvent -= onInterstitialAdLoadFailedEvent;
				IronSourceEvents.onInterstitialAdReadyEvent -= onInterstitialAdReadyEvent;
				
				IronSourceEvents.onInterstitialAdShowFailedEvent -= onInterstitialAdShowFailedEvent;
				IronSourceEvents.onInterstitialAdShowSucceededEvent -= onInterstitialAdShowSucceededEvent;
				
				IronSourceEvents.onInterstitialAdClickedEvent -= onInterstitialAdClickedEvent;
				IronSourceEvents.onInterstitialAdOpenedEvent -= onInterstitialAdOpenedEvent;
				
				IronSourceEvents.onInterstitialAdRewardedEvent -= onInterstitialAdRewardedEvent;
				IronSourceEvents.onInterstitialAdClosedEvent -= onInterstitialAdClosedEvent;
			}
			return register;
		}
		#endregion
		
		
		#region Callback
		//Invoked when the initialization process has failed.
		private void onInterstitialAdLoadFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onInterstitialAdLoadFailedEvent:{error.ToString()}",this);
			OnLoadFailBase();
		}
		
		//Invoked when the Interstitial is Ready to shown after load function is called
		private void onInterstitialAdReadyEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdReadyEvent",this);
			SetLoadedBase(true);
		}
		
		//Invoked when the ad fails to show.
		private void onInterstitialAdShowFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onInterstitialAdShowFailedEvent:{error.ToString()}",this);
			OnCloseBase();
		}
		
		//Invoked right before the Interstitial screen is about to open.
		private void onInterstitialAdShowSucceededEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdShowSucceededEvent",this);
			OnShowBase();
		}
		
		// Invoked when end user clicked on the interstitial ad
		private void onInterstitialAdClickedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdClickedEvent",this);
			OnClickBase();
		}
		
		//Invoked when the Interstitial Ad Unit has opened
		private void onInterstitialAdOpenedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdOpenedEvent",this);
			// OnVisitBase();
		}
		
		//Invoked when the user completed the video and should be rewarded. 
		private void onInterstitialAdRewardedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdRewardedEvent",this);
			OnRewardBase(1);
		}
		
		//Invoked when the interstitial ad closed and the user goes back to the application screen.
		private void onInterstitialAdClosedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onInterstitialAdClosedEvent",this);
			OnCloseBase();
			SetLoadedBase(false);
		}
		#endregion
		
		
		#region Override
		#if !UNITY_EDITOR
		protected override void NameAdapter()
		{
			nameAdepter = "IronSource";
		}
		
		protected override void Load()
		{
			if(CanDebug) Debug.Log($"{this.name}:Load",this);
			IronSource.Agent.loadInterstitial();
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
			IronSource.Agent.showInterstitial();
		}
		#endif
		#endregion
	}
}
#else
namespace xLib.libAdvert.xIronSource
{
	public class AdInterstitial : AdvertBase
	{
	}
}
#endif
#endif
#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdInterstitial : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
			if (value)
			{
				IronSourceEvents.onInterstitialAdReadyEvent += OnAdReady;
				IronSourceEvents.onInterstitialAdLoadFailedEvent += OnAdLoadFailed;
				
				IronSourceEvents.onInterstitialAdShowSucceededEvent += OnAdShowSucceeded;
				IronSourceEvents.onInterstitialAdShowFailedEvent += OnAdShowFailed;
				
				IronSourceEvents.onInterstitialAdClickedEvent += OnAdClicked;
				IronSourceEvents.onInterstitialAdOpenedEvent += OnAdOpened;
				IronSourceEvents.onInterstitialAdClosedEvent += OnAdClosed;
				
				IronSourceEvents.onInterstitialAdRewardedEvent += OnAdRewarded;
				
				OnRegisterBase();
				if(IronSource.Agent.isInterstitialReady()) OnAdReady();
			}
			else
			{
				IronSourceEvents.onInterstitialAdReadyEvent -= OnAdReady;
				IronSourceEvents.onInterstitialAdLoadFailedEvent -= OnAdLoadFailed;
				
				IronSourceEvents.onInterstitialAdShowSucceededEvent -= OnAdShowSucceeded;
				IronSourceEvents.onInterstitialAdShowFailedEvent -= OnAdShowFailed;
				
				IronSourceEvents.onInterstitialAdClickedEvent -= OnAdClicked;
				IronSourceEvents.onInterstitialAdOpenedEvent -= OnAdOpened;
				IronSourceEvents.onInterstitialAdClosedEvent -= OnAdClosed;
				
				IronSourceEvents.onInterstitialAdRewardedEvent -= OnAdRewarded;
			}
			return value;
		}
		#endregion
		
		
		#region Callback
		private void OnAdReady()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdReady",this);
			SetLoadedBase(true);
		}
		
		private void OnAdLoadFailed(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:OnAdLoadFailed:{error.ToString()}",this);
			OnLoadFailBase();
		}
		
		private void OnAdShowSucceeded()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdShowSucceeded",this);
			OnShowBase();
		}
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:OnAdShowFailed:{error.ToString()}",this);
		}
		
		private void OnAdClicked()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdClicked",this);
			OnClickBase();
		}
		
		private void OnAdOpened()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdOpened",this);
			OnVisitBase();
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdClosed",this);
			OnCloseBase();
			SetLoadedBase(false);
		}
		
		private void OnAdRewarded()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdRewarded",this);
			OnRewardBase(1);
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
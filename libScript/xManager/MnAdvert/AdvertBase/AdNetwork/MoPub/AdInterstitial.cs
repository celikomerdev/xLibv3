#if xLibv3
#if AdMoPub
using System;
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class AdInterstitial : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegister:{0}:{1}",key,value);
			
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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdReady");
			SetLoadedBase(true);
		}
		
		private void OnAdLoadFailed(IronSourceError error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnAdLoadFailed:{0}",error.ToString());
			OnLoadFailBase();
		}
		
		private void OnAdShowSucceeded()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdShowSucceeded");
			OnShowBase();
		}
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnAdShowFailed:{0}",error.ToString());
		}
		
		private void OnAdClicked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClicked");
			OnClickBase();
		}
		
		private void OnAdOpened()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdOpened");
			OnVisitBase();
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClosed");
			OnCloseBase();
			SetLoadedBase(false);
		}
		
		private void OnAdRewarded()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdRewarded");
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
			if(CanDebug) Debug.LogFormat(this,this.name+":Load");
			IronSource.Agent.loadInterstitial();
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Show");
			IronSource.Agent.showInterstitial();
		}
		#endif
		#endregion
	}
}
#else
namespace xLib.libAdvert.xMoPub
{
	public class AdInterstitial : AdvertBase
	{
	}
}
#endif
#endif
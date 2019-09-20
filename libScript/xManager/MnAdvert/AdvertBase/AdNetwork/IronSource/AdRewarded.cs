#if xLibv3
#if AdIronSource
using System;
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdRewarded : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegister:{0}:{1}",key,value);
			
			if (value)
			{
				IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += OnAdAvailabilityChanged;
				
				IronSourceEvents.onRewardedVideoAdOpenedEvent += OnAdOpened;
				IronSourceEvents.onRewardedVideoAdStartedEvent += OnAdStarted;
				
				IronSourceEvents.onRewardedVideoAdEndedEvent += OnAdEnded;
				IronSourceEvents.onRewardedVideoAdClosedEvent += OnAdClosed;
				
				IronSourceEvents.onRewardedVideoAdRewardedEvent += OnAdRewarded;
				IronSourceEvents.onRewardedVideoAdShowFailedEvent += OnAdShowFailed;
				
				OnRegisterBase();
				OnAdAvailabilityChanged(IronSource.Agent.isRewardedVideoAvailable());
			}
			else
			{
				IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= OnAdAvailabilityChanged;
				
				IronSourceEvents.onRewardedVideoAdOpenedEvent -= OnAdOpened;
				IronSourceEvents.onRewardedVideoAdStartedEvent -= OnAdStarted;
				
				IronSourceEvents.onRewardedVideoAdEndedEvent -= OnAdEnded;
				IronSourceEvents.onRewardedVideoAdClosedEvent -= OnAdClosed;
				
				IronSourceEvents.onRewardedVideoAdRewardedEvent -= OnAdRewarded;
				IronSourceEvents.onRewardedVideoAdShowFailedEvent -= OnAdShowFailed;
			}
			return value;
		}
		#endregion
		
		
		#region Callback
		private void OnAdAvailabilityChanged(bool status)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdAvailabilityChanged:{0}",status);
			SetLoadedBase(status);
		}
		
		private void OnAdOpened()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdOpened");
			OnShowBase();
		}
		
		private void OnAdStarted()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdStarted");
		}
		
		private void OnAdEnded()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdEnded");
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClosed");
			OnCloseBase();
		}
		
		private void OnAdRewarded(IronSourcePlacement placement)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdRewarded:{0}",placement.ToString());
			int prize = placement.getRewardAmount();
			OnRewardBase(prize);
		}
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnAdShowFailed:{0}",error.ToString());
		}
		#endregion
		
		
		#region Override
		protected override void NameAdapter()
		{
			nameAdepter = "IronSource";
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Show");
			IronSource.Agent.showRewardedVideo();
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xIronSource
{
	public class AdRewarded : AdvertBase
	{
	}
}
#endif
#endif
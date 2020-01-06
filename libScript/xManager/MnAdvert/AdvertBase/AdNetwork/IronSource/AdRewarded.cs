#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdRewarded : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
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
			if(CanDebug) Debug.Log($"{this.name}:OnAdAvailabilityChanged:{status}",this);
			SetLoadedBase(status);
		}
		
		private void OnAdOpened()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdOpened",this);
			OnShowBase();
		}
		
		private void OnAdStarted()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdStarted",this);
		}
		
		private void OnAdEnded()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdEnded",this);
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdClosed",this);
			OnCloseBase();
		}
		
		private void OnAdRewarded(IronSourcePlacement placement)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdRewarded:{placement.ToString()}",this);
			int prize = placement.getRewardAmount();
			OnRewardBase(prize);
		}
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:OnAdShowFailed:{error.ToString()}",this);
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
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
			IronSource.Agent.showRewardedVideo();
		}
		#endif
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
#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdRewarded : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{register}",this);
			
			if (register)
			{
				IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += onRewardedVideoAvailabilityChangedEvent;
				
				IronSourceEvents.onRewardedVideoAdShowFailedEvent += onRewardedVideoAdShowFailedEvent;
				IronSourceEvents.onRewardedVideoAdOpenedEvent += onRewardedVideoAdOpenedEvent;
				
				IronSourceEvents.onRewardedVideoAdStartedEvent += onRewardedVideoAdStartedEvent;
				IronSourceEvents.onRewardedVideoAdEndedEvent += onRewardedVideoAdEndedEvent;
				
				IronSourceEvents.onRewardedVideoAdRewardedEvent += onRewardedVideoAdRewardedEvent;
				IronSourceEvents.onRewardedVideoAdClosedEvent += onRewardedVideoAdClosedEvent;
				
				OnRegisterBase();
				onRewardedVideoAvailabilityChangedEvent(IronSource.Agent.isRewardedVideoAvailable());
			}
			else
			{
				IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= onRewardedVideoAvailabilityChangedEvent;
				
				IronSourceEvents.onRewardedVideoAdShowFailedEvent -= onRewardedVideoAdShowFailedEvent;
				IronSourceEvents.onRewardedVideoAdOpenedEvent -= onRewardedVideoAdOpenedEvent;
				
				IronSourceEvents.onRewardedVideoAdStartedEvent -= onRewardedVideoAdStartedEvent;
				IronSourceEvents.onRewardedVideoAdEndedEvent -= onRewardedVideoAdEndedEvent;
				
				IronSourceEvents.onRewardedVideoAdRewardedEvent -= onRewardedVideoAdRewardedEvent;
				IronSourceEvents.onRewardedVideoAdClosedEvent -= onRewardedVideoAdClosedEvent;
			}
			return register;
		}
		#endregion
		
		
		#region Callback
		//Invoked when there is a change in the ad availability status.
		private void onRewardedVideoAvailabilityChangedEvent(bool status)
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAvailabilityChangedEvent:{status}",this);
			SetLoadedBase(status);
		}
		
		//Invoked when the Rewarded Video failed to show
		private void onRewardedVideoAdShowFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onRewardedVideoAdShowFailedEvent:{error.ToString()}",this);
			OnCloseBase();
		}
		
		//Invoked when the RewardedVideo ad view has opened.
		private void onRewardedVideoAdOpenedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAdOpenedEvent",this);
			OnShowBase();
		}
		
		//Invoked when the video ad starts playing.
		private void onRewardedVideoAdStartedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAdStartedEvent",this);
		}
		
		//Invoked when the video ad finishes playing.
		private void onRewardedVideoAdEndedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAdEndedEvent",this);
		}
		
		//Invoked when the user completed the video and should be rewarded. 
		private void onRewardedVideoAdRewardedEvent(IronSourcePlacement placement)
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAdRewardedEvent:{placement.ToString()}",this);
			int prize = placement.getRewardAmount();
			OnRewardBase(prize);
		}
		
		//Invoked when the RewardedVideo ad view is about to be closed.
		private void onRewardedVideoAdClosedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onRewardedVideoAdClosedEvent",this);
			OnCloseBase();
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
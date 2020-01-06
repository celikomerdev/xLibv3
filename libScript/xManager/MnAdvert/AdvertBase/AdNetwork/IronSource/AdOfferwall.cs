#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdOfferwall : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
			if (value)
			{
				IronSourceEvents.onOfferwallAvailableEvent += OnAdAvailabilityChanged;
				IronSourceEvents.onOfferwallOpenedEvent += OnAdOpened;
				IronSourceEvents.onOfferwallShowFailedEvent += OnAdShowFailed;
				IronSourceEvents.onOfferwallClosedEvent += OnAdClosed;
				
				IronSourceEvents.onOfferwallAdCreditedEvent += OnAdRewarded;
				IronSourceEvents.onGetOfferwallCreditsFailedEvent += OnAdRewardFailed;
				
				OnRegisterBase();
				OnAdAvailabilityChanged(IronSource.Agent.isOfferwallAvailable());
			}
			else
			{
				IronSourceEvents.onOfferwallAvailableEvent -= OnAdAvailabilityChanged;
				IronSourceEvents.onOfferwallOpenedEvent -= OnAdOpened;
				IronSourceEvents.onOfferwallShowFailedEvent -= OnAdShowFailed;
				IronSourceEvents.onOfferwallClosedEvent -= OnAdClosed;
				
				IronSourceEvents.onOfferwallAdCreditedEvent -= OnAdRewarded;
				IronSourceEvents.onGetOfferwallCreditsFailedEvent -= OnAdRewardFailed;
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
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:OnAdShowFailed:{error.ToString()}",this);
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdClosed",this);
			OnCloseBase();
		}
		
		private void OnAdRewarded(System.Collections.Generic.Dictionary<string, object> rewards)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdRewarded:{rewards.ToJsonString()}",this);
			
			if(rewards.ContainsKey("totalCreditsFlag"))
			{
				if((string)rewards["totalCreditsFlag"] == "true") return;
			}
			
			if(!rewards.ContainsKey("credits")) return;
			int prize = int.Parse((string)rewards["credits"]);
			OnRewardBase(prize);
		}
		
		private void OnAdRewardFailed(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:OnAdRewardFailed:{error.ToString()}",this);
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
			if(string.IsNullOrEmpty(key)) IronSource.Agent.showOfferwall();
			else IronSource.Agent.showOfferwall(key);
		}
		#endif
		#endregion
		
		
		#region Custom
		public void RefreshRewards()
		{
			if(CanDebug) Debug.Log($"{this.name}:RefreshRewards",this);
			IronSource.Agent.getOfferwallCredits();
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xIronSource
{
	public class AdOfferwall : AdvertBase
	{
		#region Custom
		public void RefreshRewards(){}
		#endregion
	}
}
#endif
#endif
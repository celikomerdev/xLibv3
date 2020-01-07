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
				IronSourceEvents.onOfferwallAvailableEvent += onOfferwallAvailableEvent;
				
				IronSourceEvents.onOfferwallShowFailedEvent += onOfferwallShowFailedEvent;
				IronSourceEvents.onOfferwallOpenedEvent += onOfferwallOpenedEvent;
				
				IronSourceEvents.onGetOfferwallCreditsFailedEvent += onGetOfferwallCreditsFailedEvent;
				IronSourceEvents.onOfferwallAdCreditedEvent += onOfferwallAdCreditedEvent;
				
				IronSourceEvents.onOfferwallClosedEvent += onOfferwallClosedEvent;
				
				OnRegisterBase();
				onOfferwallAvailableEvent(IronSource.Agent.isOfferwallAvailable());
			}
			else
			{
				IronSourceEvents.onOfferwallAvailableEvent -= onOfferwallAvailableEvent;
				
				IronSourceEvents.onOfferwallShowFailedEvent -= onOfferwallShowFailedEvent;
				IronSourceEvents.onOfferwallOpenedEvent -= onOfferwallOpenedEvent;
				
				IronSourceEvents.onGetOfferwallCreditsFailedEvent -= onGetOfferwallCreditsFailedEvent;
				IronSourceEvents.onOfferwallAdCreditedEvent -= onOfferwallAdCreditedEvent;
				
				IronSourceEvents.onOfferwallClosedEvent -= onOfferwallClosedEvent;
			}
			return value;
		}
		#endregion
		
		
		#region Callback
		// Invoked when there is a change in the Offerwall availability status.
		private void onOfferwallAvailableEvent(bool status)
		{
			if(CanDebug) Debug.Log($"{this.name}:onOfferwallAvailableEvent:{status}",this);
			SetLoadedBase(status);
		}
		
		// Invoked when the method 'showOfferWall' is called and the OfferWall fails to load.
		private void onOfferwallShowFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onOfferwallShowFailedEvent:{error.ToString()}",this);
			OnCloseBase();
		}
		
		// Invoked when the Offerwall successfully loads for the user.
		private void onOfferwallOpenedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onOfferwallOpenedEvent",this);
			OnShowBase();
		}
		
		// Invoked when the method 'getOfferWallCredits' fails to retrieve the user's credit balance info.
		private void onGetOfferwallCreditsFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onGetOfferwallCreditsFailedEvent:{error.ToString()}",this);
			OnRewardBase(0);
		}
		
		// Invoked each time the user completes an offer.
		private void onOfferwallAdCreditedEvent(System.Collections.Generic.Dictionary<string, object> rewards)
		{
			if(CanDebug) Debug.Log($"{this.name}:onOfferwallAdCreditedEvent:{rewards.ToJsonString()}",this);
			
			if(rewards.ContainsKey("totalCreditsFlag"))
			{
				if((string)rewards["totalCreditsFlag"] == "true") return;
			}
			
			if(!rewards.ContainsKey("credits")) return;
			int prize = int.Parse((string)rewards["credits"]);
			OnRewardBase(prize);
		}
		
		// Invoked when the user is about to return to the application after closing the Offerwall.
		private void onOfferwallClosedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onOfferwallClosedEvent",this);
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
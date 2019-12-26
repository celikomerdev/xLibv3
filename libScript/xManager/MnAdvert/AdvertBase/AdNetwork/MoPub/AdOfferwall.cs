#if xLibv3
#if AdMoPub
using System;
using System.Collections.Generic;
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class AdOfferwall : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegister:{0}:{1}",key,value);
			
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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdAvailabilityChanged:{0}",status);
			SetLoadedBase(status);
		}
		
		private void OnAdOpened()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdOpened");
			OnShowBase();
		}
		
		private void OnAdShowFailed(IronSourceError error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnAdShowFailed:{0}",error.ToString());
		}
		
		private void OnAdClosed()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClosed");
			OnCloseBase();
		}
		
		private void OnAdRewarded(Dictionary<string,object> rewards)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdRewarded:{0}",Newtonsoft.Json.JsonConvert.SerializeObject(rewards));
			
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
			xDebug.LogExceptionFormat(this,this.name+":OnAdRewardFailed:{0}",error.ToString());
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
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Show");
			if(string.IsNullOrEmpty(key)) IronSource.Agent.showOfferwall();
			else IronSource.Agent.showOfferwall(key);
		}
		#endif
		#endregion
		
		
		#region Custom
		public void RefreshRewards()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":RefreshRewards");
			IronSource.Agent.getOfferwallCredits();
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xMoPub
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
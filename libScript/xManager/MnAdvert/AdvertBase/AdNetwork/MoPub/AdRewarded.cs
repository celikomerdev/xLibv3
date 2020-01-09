﻿#if xLibv3
#if AdMoPub
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class AdRewarded : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{register}",this);
			
			if (register)
			{
				MoPub.LoadRewardedVideoPluginsForAdUnits(new string[1]{idPlatform});
				MoPubManager.OnImpressionTrackedEvent += OnImpressionTrackedEvent;
				MoPubManager.OnRewardedVideoLoadedEvent += OnRewardedVideoLoadedEvent;
				MoPubManager.OnRewardedVideoFailedEvent += OnRewardedVideoFailedEvent;
				MoPubManager.OnRewardedVideoFailedToPlayEvent += OnRewardedVideoFailedToPlayEvent;
				MoPubManager.OnRewardedVideoShownEvent += OnRewardedVideoShownEvent;
				MoPubManager.OnRewardedVideoClickedEvent += OnRewardedVideoClickedEvent;
				MoPubManager.OnRewardedVideoLeavingApplicationEvent += OnRewardedVideoLeavingApplicationEvent;
				MoPubManager.OnRewardedVideoClosedEvent += OnRewardedVideoClosedEvent;
				MoPubManager.OnRewardedVideoReceivedRewardEvent += OnRewardedVideoReceivedRewardEvent;
				MoPubManager.OnRewardedVideoExpiredEvent += OnRewardedVideoExpiredEvent;
				
				OnRegisterBase();
				if(MnMoPub.isSdkInit)
				{
					if(MoPub.HasRewardedVideo(idPlatform)) OnRewardedVideoLoadedEvent(idPlatform);
				}
			}
			else
			{
				MoPubManager.OnImpressionTrackedEvent -= OnImpressionTrackedEvent;
				MoPubManager.OnRewardedVideoLoadedEvent -= OnRewardedVideoLoadedEvent;
				MoPubManager.OnRewardedVideoFailedEvent -= OnRewardedVideoFailedEvent;
				MoPubManager.OnRewardedVideoFailedToPlayEvent -= OnRewardedVideoFailedToPlayEvent;
				MoPubManager.OnRewardedVideoShownEvent -= OnRewardedVideoShownEvent;
				MoPubManager.OnRewardedVideoClickedEvent -= OnRewardedVideoClickedEvent;
				MoPubManager.OnRewardedVideoLeavingApplicationEvent -= OnRewardedVideoLeavingApplicationEvent;
				MoPubManager.OnRewardedVideoClosedEvent -= OnRewardedVideoClosedEvent;
				MoPubManager.OnRewardedVideoReceivedRewardEvent -= OnRewardedVideoReceivedRewardEvent;
				MoPubManager.OnRewardedVideoExpiredEvent -= OnRewardedVideoExpiredEvent;
			}
			return register;
		}
		#endregion
		
		
		#region Callback
		private void OnImpressionTrackedEvent(string adUnitId,MoPub.ImpressionData impressionData)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnImpressionTrackedEvent:{adUnitId}:{impressionData.JsonRepresentation}",this);
			nameAdepter = impressionData.NetworkName;
		}
		
		private void OnRewardedVideoLoadedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoLoadedEvent:{adUnitId}",this);
			SetLoadedBase(true);
		}
		
		private void OnRewardedVideoFailedEvent(string adUnitId,string errorMsg)
		{
			if(adUnitId!=idPlatform) return;
			xLogger.LogException($"{this.name}:OnRewardedVideoFailedEvent:{adUnitId}:{errorMsg}",this);
			OnLoadFailBase();
		}
		
		private void OnRewardedVideoFailedToPlayEvent(string adUnitId,string errorMsg)
		{
			if(adUnitId!=idPlatform) return;
			xLogger.LogException($"{this.name}:OnRewardedVideoFailedToPlayEvent:{adUnitId}:{errorMsg}",this);
		}
		
		private void OnRewardedVideoShownEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoShownEvent:{adUnitId}",this);
			OnShowBase();
		}
		
		private void OnRewardedVideoClickedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoClickedEvent:{adUnitId}",this);
			OnClickBase();
		}
		
		private void OnRewardedVideoLeavingApplicationEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoLeavingApplicationEvent:{adUnitId}",this);
			OnVisitBase();
		}
		
		private void OnRewardedVideoClosedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoClosedEvent:{adUnitId}",this);
			OnCloseBase();
		}
		
		private void OnRewardedVideoReceivedRewardEvent(string adUnitId,string label,float amount)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoReceivedRewardEvent:{adUnitId}:{label}:{amount}",this);
			int prize = Mathf.RoundToInt(amount);
			OnRewardBase(prize);
		}
		
		private void OnRewardedVideoExpiredEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnRewardedVideoExpiredEvent:{adUnitId}",this);
			SetLoadedBase(false);
			LoadBase();
		}
		#endregion
		
		
		#region Override
		#if !UNITY_EDITOR
		protected override void Load()
		{
			if(CanDebug) Debug.Log($"{this.name}:Load",this);
			if(!MnMoPub.isSdkInit)
			{
				OnLoadFailBase();
				return;
			}
			if(MoPub.HasRewardedVideo(idPlatform))
			{
				OnRewardedVideoLoadedEvent(idPlatform);
				return;
			}
			MoPub.RequestRewardedVideo(idPlatform);
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
			MoPub.ShowRewardedVideo(idPlatform);
		}
		#endif
		#endregion
	}
}
#else
namespace xLib.libAdvert.xMoPub
{
	public class AdRewarded : AdvertBase
	{
	}
}
#endif
#endif
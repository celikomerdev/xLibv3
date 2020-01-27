#if xLibv3
#if AdMoPub
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class AdInterstitial : AdvertBase
	{
		#region TryRegister
		protected override bool TryRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:TryRegister:{key}:{register}",this);
			
			if (register)
			{
				MoPub.LoadInterstitialPluginsForAdUnits(new string[1]{idPlatform});
				MoPubManager.OnImpressionTrackedEvent += OnImpressionTrackedEvent;
				MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
				MoPubManager.OnInterstitialFailedEvent += OnInterstitialFailedEvent;
				MoPubManager.OnInterstitialShownEvent += OnInterstitialShownEvent;
				MoPubManager.OnInterstitialClickedEvent += OnInterstitialClickedEvent;
				MoPubManager.OnInterstitialDismissedEvent += OnInterstitialDismissedEvent;
				MoPubManager.OnInterstitialExpiredEvent += OnInterstitialExpiredEvent;
				
				OnRegisteredBase();
				if(MnMoPub.isSdkInit)
				{
					if(MoPub.IsInterstitialReady(idPlatform)) OnInterstitialLoadedEvent(idPlatform);
				}
			}
			else
			{
				MoPubManager.OnImpressionTrackedEvent += OnImpressionTrackedEvent;
				MoPubManager.OnInterstitialLoadedEvent -= OnInterstitialLoadedEvent;
				MoPubManager.OnInterstitialFailedEvent -= OnInterstitialFailedEvent;
				MoPubManager.OnInterstitialShownEvent -= OnInterstitialShownEvent;
				MoPubManager.OnInterstitialClickedEvent -= OnInterstitialClickedEvent;
				MoPubManager.OnInterstitialDismissedEvent -= OnInterstitialDismissedEvent;
				MoPubManager.OnInterstitialExpiredEvent -= OnInterstitialExpiredEvent;
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
		
		private void OnInterstitialLoadedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialLoadedEvent:{adUnitId}",this);
			SetLoadedBase(true);
		}
		
		private void OnInterstitialFailedEvent(string adUnitId,string errorCode)
		{
			if(adUnitId!=idPlatform) return;
			xLogger.LogException($"{this.name}:OnInterstitialFailedEvent:{adUnitId}:{errorCode}",this);
			OnLoadFailBase();
		}
		
		private void OnInterstitialShownEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialShownEvent:{adUnitId}",this);
			OnShowBase();
		}
		
		private void OnInterstitialClickedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialClickedEvent:{adUnitId}",this);
			OnClickBase();
		}
		
		private void OnInterstitialDismissedEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialDismissedEvent:{adUnitId}",this);
			OnCloseBase();
		}
		
		private void OnInterstitialExpiredEvent(string adUnitId)
		{
			if(adUnitId!=idPlatform) return;
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialExpiredEvent:{adUnitId}",this);
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
			if(MoPub.IsInterstitialReady(idPlatform))
			{
				OnInterstitialLoadedEvent(idPlatform);
				return;
			}
			MoPub.RequestInterstitialAd(idPlatform);
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
			MoPub.ShowInterstitialAd(idPlatform);
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
#if xLibv3
#if AdMoPub
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class AdInterstitial : AdvertBase
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
			if (value)
			{
				MoPub.LoadInterstitialPluginsForAdUnits(new string[1]{idPlatform});
				MoPubManager.OnImpressionTrackedEvent += OnImpressionTrackedEvent;
				MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
				MoPubManager.OnInterstitialFailedEvent += OnInterstitialFailedEvent;
				MoPubManager.OnInterstitialShownEvent += OnInterstitialShownEvent;
				MoPubManager.OnInterstitialClickedEvent += OnInterstitialClickedEvent;
				MoPubManager.OnInterstitialDismissedEvent += OnInterstitialDismissedEvent;
				MoPubManager.OnInterstitialExpiredEvent += OnInterstitialExpiredEvent;
				
				OnRegisterBase();
				if(MoPub.IsInterstitialReady(idPlatform)) OnInterstitialLoadedEvent(idPlatform);
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
			return value;
		}
		#endregion
		
		
		#region Callback
		private void OnImpressionTrackedEvent(string adUnitId,MoPub.ImpressionData impressionData)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnImpressionTrackedEvent:{adUnitId}:{impressionData.JsonRepresentation}",this);
			nameAdepter = impressionData.NetworkName;
		}
		
		private void OnInterstitialLoadedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialLoadedEvent:{adUnitId}",this);
			SetLoadedBase(true);
		}
		
		private void OnInterstitialFailedEvent(string adUnitId,string errorCode)
		{
			xDebug.LogException($"{this.name}:OnInterstitialFailedEvent:{adUnitId}:{errorCode}",this);
			OnLoadFailBase();
		}
		
		private void OnInterstitialShownEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialShownEvent:{adUnitId}",this);
			OnShowBase();
		}
		
		private void OnInterstitialClickedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialClickedEvent:{adUnitId}",this);
			OnClickBase();
		}
		
		private void OnInterstitialDismissedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInterstitialDismissedEvent:{adUnitId}",this);
			OnCloseBase();
			SetLoadedBase(false);
		}
		
		private void OnInterstitialExpiredEvent(string adUnitId)
		{
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
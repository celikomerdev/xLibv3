#if xLibv3
#if AdMoPub
using System;
using UnityEngine;
using static MoPub;

namespace xLib.libAdvert.xMoPub
{
	public class AdBanner : AdvertBaseBanner
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
			if (value)
			{
				MoPub.LoadBannerPluginsForAdUnits(new string[1]{idPlatform});
				MoPubManager.OnImpressionTrackedEvent += OnImpressionTrackedEvent;
				MoPubManager.OnAdLoadedEvent += OnAdLoadedEvent;
				MoPubManager.OnAdFailedEvent += OnAdFailedEvent;
				MoPubManager.OnAdClickedEvent += OnAdClickedEvent;
				MoPubManager.OnAdExpandedEvent += OnAdExpandedEvent;
				MoPubManager.OnAdCollapsedEvent += OnAdCollapsedEvent;
				OnRegisterBase();
			}
			else
			{
				MoPubManager.OnImpressionTrackedEvent -= OnImpressionTrackedEvent;
				MoPubManager.OnAdLoadedEvent -= OnAdLoadedEvent;
				MoPubManager.OnAdFailedEvent -= OnAdFailedEvent;
				MoPubManager.OnAdClickedEvent -= OnAdClickedEvent;
				MoPubManager.OnAdExpandedEvent -= OnAdExpandedEvent;
				MoPubManager.OnAdCollapsedEvent -= OnAdCollapsedEvent;
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
		
		private void OnAdLoadedEvent(string adUnitId,float height)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdLoadedEvent:{adUnitId}:height:{height}",this);
			SetLoadedBase(true);
			height = Mathf.CeilToInt(height);
			IsDisplay = isDisplay;
		}
		
		private void OnAdFailedEvent(string adUnitId,string error)
		{
			xDebug.LogException($"{this.name}:OnAdLoadedEvent:{adUnitId}:error:{error}",this);
			OnLoadFailBase();
		}
		
		private void OnAdClickedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdClickedEvent:{adUnitId}",this);
			OnClickBase();
		}
		
		private void OnAdExpandedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdExpandedEvent:{adUnitId}",this);
			OnShowBase();
		}
		
		private void OnAdCollapsedEvent(string adUnitId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnAdCollapsedEvent:{adUnitId}",this);
			OnCloseBase();
		}
		#endregion
		
		
		#region Override
		#if UNITY_EDITOR
		protected override void Load()
		{
			if(CanDebug) Debug.Log($"{this.name}:Load",this);
			MoPub.RequestBanner(idPlatform,xAdPosition,xAdSize);
			DebugBanner();
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
			MoPub.ShowBanner(idPlatform,true);
			OnDisplay = true;
			OnWidth = width*multiplierPixel;
			OnHeight = height*multiplierPixel;
		}
		
		protected override void Hide()
		{
			MoPub.ShowBanner(idPlatform,false);
			base.Hide();
		}
		#endif
		#endregion
		
		
		#region Custom
		private MaxAdSize xAdSize
		{
			get
			{
				width = Mathf.FloorToInt(Screen.width/multiplierPixel);
				height = Mathf.FloorToInt(Screen.height/multiplierPixel);
				
				float heightInc = ExtScreen.HeightInc;
				MaxAdSize returnObj = MaxAdSize.ScreenWidthHeight90;
				Enum.TryParse(adPosition,out MaxAdSize myStatus);
				
				width = Mathf.CeilToInt(AdSizeMapping.Width(returnObj));
				height = Mathf.CeilToInt(AdSizeMapping.Height(returnObj));
				
				return returnObj;
			}
		}
		
		// public enum AdPosition{TopLeft,TopCenter,TopRight,Centered,BottomLeft,BottomCenter,BottomRight}
		private AdPosition xAdPosition
		{
			get
			{
				AdPosition returnObj = AdPosition.BottomCenter;
				Enum.TryParse(adPosition,out AdPosition myStatus);
				return returnObj;
			}
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xMoPub
{
	public class AdBanner : AdvertBaseBanner
	{
	}
}
#endif
#endif
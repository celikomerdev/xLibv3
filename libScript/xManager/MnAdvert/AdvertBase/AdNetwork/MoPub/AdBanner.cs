#if xLibv3
#if AdMoPub
using UnityEngine;

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
			xDebug.LogException($"{this.name}:OnAdFailedEvent:{adUnitId}:error:{error}",this);
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
		#if !UNITY_EDITOR
		protected override void Load()
		{
			if(CanDebug) Debug.Log($"{this.name}:Load",this);
			if(!MnMoPub.isSdkInit)
			{
				OnLoadFailBase();
				return;
			}
			MoPub.RequestBanner(idPlatform,AdPosition,AdSize);
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
		private MoPub.AdPosition AdPosition
		{
			get
			{
				MoPub.AdPosition returnObj = MoPub.AdPosition.BottomCenter;
				if(System.Enum.TryParse(adPosition,out MoPub.AdPosition outObj)) returnObj = outObj;
				
				if(CanDebug) Debug.Log($"{this.name}:AdPosition:{returnObj.ToString()}",this);
				return returnObj;
			}
		}
		// public enum AdPosition
		// {
		// 	TopLeft,
		// 	TopCenter,
		// 	TopRight,
		// 	Centered,
		// 	BottomLeft,
		// 	BottomCenter,
		// 	BottomRight
		// }
		
		
		private MoPub.MaxAdSize AdSize
		{
			get
			{
				MoPub.MaxAdSize returnObj = MoPub.MaxAdSize.ScreenWidthHeight90;
				if(System.Enum.TryParse(adSize,out MoPub.MaxAdSize outObj)) returnObj = outObj;
				
				width = Mathf.CeilToInt(AdSizeMapping.Width(returnObj));
				height = Mathf.CeilToInt(AdSizeMapping.Height(returnObj));
				
				if(CanDebug) Debug.Log($"{this.name}:AdSize:{returnObj.ToString()}",this);
				return returnObj;
			}
		}
		// public enum MaxAdSize
		// {
		// 	Width300Height50,
		// 	Width300Height250,
		// 	Width320Height50,
		// 	Width336Height280,
		// 	Width468Height60,
		// 	Width728Height90,
		// 	Width970Height90,
		// 	Width970Height250,
		// 	ScreenWidthHeight50,
		// 	ScreenWidthHeight90,
		// 	ScreenWidthHeight250,
		// 	ScreenWidthHeight280
		// }
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
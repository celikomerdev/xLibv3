#if xLibv3
#if AdGoogle
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using xLib.ToolEventClass;

namespace xLib.libAdvert.xGoogleAds
{
	public class AdBanner : AdvertBaseBanner
	{
		private BannerView advert;
		private AdRequest request;
		
		#region Register
		protected override bool Register(bool value)
		{
			if (value)
			{
				IdPlatform();
				advert = new BannerView(idPlatform,xAdSize,xAdPosition);
				
				advert.OnAdLoaded += OnAdLoaded;
				advert.OnAdFailedToLoad += OnAdFailedToLoad;
				advert.OnAdOpening += OnAdOpening;
				advert.OnAdLeavingApplication += OnAdLeavingApplication;
				advert.OnAdClosed += OnAdClosed;
				
				AdRequest.Builder builder = MnGoogleAds.ins.Builder();
				request = builder.Build();
				OnRegisterBase();
			}
			else
			{
				advert.OnAdLoaded -= OnAdLoaded;
				advert.OnAdFailedToLoad -= OnAdFailedToLoad;
				advert.OnAdOpening -= OnAdOpening;
				advert.OnAdLeavingApplication -= OnAdLeavingApplication;
				advert.OnAdClosed -= OnAdClosed;
				
				advert.Destroy();
				advert = null;
			}
			return value;
		}
		#endregion
		
		
		#region Callback
		private void OnAdLoaded(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdLoaded:{0}",args.ToString());
			SetLoadedBase(true);
			IsDisplay = isDisplay;
		}
		
		private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdFailedToLoad:{0}",args.ToString());
			OnLoadFailBase();
		}
		
		private void OnAdOpening(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdOpening:{0}",args.ToString());
			//Clicked to play/install
		}
		
		private void OnAdLeavingApplication(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdLeavingApplication:{0}",args.ToString());
			OnClickBase();
		}
		
		private void OnAdClosed(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClosed:{0}",args.ToString());
			OnCloseBase();
		}
		#endregion
		
		
		#region Override
		protected override void NameAdapter()
		{
			nameAdepter = advert.MediationAdapterClassName();
		}
		
		protected override void Load()
		{
			advert.LoadAd(request);
		}
		
		protected override void Show()
		{
			advert.Show();
			OnDisplay = true;
			OnWidth = advert.GetWidthInPixels();
			OnHeight = advert.GetHeightInPixels();
		}
		
		protected override void Hide()
		{
			advert.Hide();
			base.Hide();
		}
		#endregion
		
		#region Custom
		private AdPosition xAdPosition
		{
			get
			{
				switch (adPosition)
				{
					case "Top":
						return AdPosition.Top;
					case "Bottom":
						return AdPosition.Bottom;
					case "TopLeft":
						return AdPosition.TopLeft;
					case "TopRight":
						return AdPosition.TopRight;
					case "BottomLeft":
						return AdPosition.BottomLeft;
					case "BottomRight":
						return AdPosition.BottomRight;
					case "Center":
						return AdPosition.Center;
					default:
						return AdPosition.Bottom;
				}
			}
		}
		
		private AdSize xAdSize
		{
			get
			{
				switch (adSize)
				{
					case "Banner":
						return AdSize.Banner;
					case "MediumRectangle":
						return AdSize.MediumRectangle;
					case "IABBanner":
						return AdSize.IABBanner;
					case "Leaderboard":
						return AdSize.Leaderboard;
					case "SmartBanner":
						return AdSize.SmartBanner;
					default:
						return AdSize.SmartBanner;
				}
			}
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xGoogleAds
{
	public class AdBanner : AdvertBaseBanner
	{
	}
}
#endif
#endif
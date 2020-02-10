#if xLibv3
#if AdGoogle
using System;
using UnityEngine;
using GoogleMobileAds.Api;

#if MedVungle
using GoogleMobileAds.Api.Mediation.Vungle;
#endif

namespace xLib.libAdvert.xGoogleAds
{
	public class AdInterstitial : AdvertBase
	{
		private InterstitialAd advert;
		private AdRequest request;
		
		
		#region TryRegister
		protected override bool TryRegister(bool value)
		{
			if (value)
			{
				IdPlatform();
				advert = new InterstitialAd(idPlatform);
				
				advert.OnAdLoaded += OnAdLoaded;
				advert.OnAdFailedToLoad += OnAdFailedToLoad;
				advert.OnAdOpening += OnAdOpening;
				advert.OnAdLeavingApplication += OnAdLeavingApplication;
				advert.OnAdClosed += OnAdClosed;
				
				AdRequest.Builder builder = MnGoogleAds.ins.Builder();
				
				#if MedVungle
				VungleInterstitialMediationExtras extrasVungle = new VungleInterstitialMediationExtras();
				extrasVungle.SetAllPlacements(new string[] { MnKey.GetValue("Vungle_Interstitial") });
				builder.AddMediationExtras(extrasVungle);
				#endif
				
				request = builder.Build();
				OnRegisteredBase();
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
		}
		
		private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdFailedToLoad:{0}",args.ToString());
			OnLoadFailBase();
		}
		
		private void OnAdOpening(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdOpening:{0}",args.ToString());
			OnShowBase();
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
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xGoogleAds
{
	public class AdInterstitial : AdvertBase
	{
	}
}
#endif
#endif
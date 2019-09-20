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
	public class AdRewarded : AdvertBase
	{
		private RewardBasedVideoAd advert;
		private AdRequest request;
		
		
		#region Register
		protected override bool Register(bool value)
		{
			if (value)
			{
				IdPlatform();
				advert = RewardBasedVideoAd.Instance;
				
				advert.OnAdLoaded += OnAdLoaded;
				advert.OnAdFailedToLoad += OnAdFailedToLoad;
				advert.OnAdOpening += OnAdOpening;
				advert.OnAdStarted += OnAdStarted;
				advert.OnAdCompleted += OnAdCompleted;
				advert.OnAdRewarded += OnAdRewarded;
				advert.OnAdLeavingApplication += OnAdLeavingApplication;
				advert.OnAdClosed += OnAdClosed;
				
				AdRequest.Builder builder = MnGoogleAds.ins.Builder();
				
				#if MedVungle
				VungleRewardedVideoMediationExtras extrasVungle = new VungleRewardedVideoMediationExtras();
				extrasVungle.SetAllPlacements(new string[] { MnKey.GetValue("Vungle-Rewarded") });
				builder.AddMediationExtras(extrasVungle);
				#endif
				
				request = builder.Build();
				OnRegisterBase();
			}
			else
			{
				advert.OnAdLoaded -= OnAdLoaded;
				advert.OnAdFailedToLoad -= OnAdFailedToLoad;
				advert.OnAdOpening -= OnAdOpening;
				advert.OnAdStarted -= OnAdStarted;
				advert.OnAdCompleted -= OnAdCompleted;
				advert.OnAdRewarded -= OnAdRewarded;
				advert.OnAdLeavingApplication -= OnAdLeavingApplication;
				advert.OnAdClosed -= OnAdClosed;
				
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
		
		private void OnAdStarted(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdStarted:{0}",args.ToString());
		}
		
		private void OnAdCompleted(object sender, EventArgs args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdCompleted:{0}",args.ToString());
		}
		
		private void OnAdRewarded(object sender, Reward args)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdRewarded:{0}",args.ToString());
			OnRewardBase();
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
			advert.LoadAd(request,idPlatform);
		}
		
		protected override void Show()
		{
			advert.Show();
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib.libAdvert.xGoogleAds
{
	public class AdRewarded : AdvertBase
	{
	}
}
#endif
#endif
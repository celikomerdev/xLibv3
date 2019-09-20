#if DiscardxLibv1
#if AdGoogle
using System;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;

namespace xLib.libAdvert.xGoogleAds
{
	public class AdNative : AdvertBase
	{
		public string key;
		private string idPlatform;
		
		
		private NativeExpressAdView advertisement;
		private AdRequest request;
		public BannerSize bannerSize = BannerSize.SmartBanner;
		public AdPosition adPosition = AdPosition.Top;
		
		#region Mono
		private void Awake()
		{
			idPlatform = MnKey.GetValue(key);
			Debug.Log(key + ": " + idPlatform);
			if (idPlatform == null) return;
			
			advertisement = new NativeExpressAdView(idPlatform, GoogleAdsHelper.GetSize(bannerSize), adPosition);
			RegisterEvents(true);
		}
		
		private void OnDestroy()
		{
			RegisterEvents(false);
			advertisement.Destroy();
		}
		#endregion
		
		#region Callbacks
		private bool isRegistered;
		private void RegisterEvents(bool value)
		{
			if (isRegistered == value) return;
			isRegistered = value;
			Debug.Log("RegisterEvents: " + key + " " + value.ToString());
			if (value)
			{
				advertisement.OnAdLoaded += OnLoad;
				advertisement.OnAdOpening += OnShow;
				advertisement.OnAdFailedToLoad += OnLoadFail;
				advertisement.OnAdClosed += OnClose;
				advertisement.OnAdLeavingApplication += OnClick;
			}
			else
			{
				advertisement.OnAdLoaded -= OnLoad;
				advertisement.OnAdOpening -= OnShow;
				advertisement.OnAdFailedToLoad -= OnLoadFail;
				advertisement.OnAdClosed -= OnClose;
				advertisement.OnAdLeavingApplication -= OnClick;
			}
		}
		
		public UnityEvent onLoad;
		private void OnLoad(object sender, EventArgs args)
		{
			Debug.Log("onLoad: " + key);
			onLoad.Invoke();
		}
		
		public UnityEvent onLoadFail;
		private void OnLoadFail(object sender, AdFailedToLoadEventArgs args)
		{
			Debug.Log("onLoadFail: " + key);
			Debug.Log(args.Message);
			onLoadFail.Invoke();
		}
		
		public UnityEvent onShow;
		private void OnShow(object sender, EventArgs args)
		{
			Debug.Log("onShow: " + key);
			onShow.Invoke();
		}
		
		public UnityEvent onClose;
		private void OnClose(object sender, EventArgs args)
		{
			Debug.Log("onClose: " + key);
			onClose.Invoke();
		}
		
		public UnityEvent onClick;
		private void OnClick(object sender, EventArgs args)
		{
			Debug.Log("onClick: " + key);
			onClick.Invoke();
		}
		#endregion
		
		#region PublicMethods
		public void Load()
		{
			Debug.Log("Load: " + key);
			if (idPlatform == null) return;
			request = new AdRequest.Builder().Build();
			//request.TestDevices.Add(AdRequest.TestDeviceSimulator);
			//if (MnSystemInfo.ins.isTestDevice) request.TestDevices.Add(MnSystemInfo.ins.idDevice.ToUpper());
			advertisement.LoadAd(request);
		}
		
		public void Show()
		{
			Debug.Log("Show: " + key);
			if (advertisement == null) return;
			advertisement.Show();
		}
		
		public void Hide()
		{
			Debug.Log("Hide: " + key);
			if (advertisement == null) return;
			advertisement.Hide();
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib.libAdvert.xGoogleAds
{
	public class AdNative : AdvertBase
	{
		protected override bool Register(bool value)
		{
			if(CanDebug) Debug.LogWarning("!GoogleAds");
			return value;
		}
	}
}
#endif
#endif
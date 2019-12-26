#if xLibv3
#if AdMoPub
using System;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.libAdvert.xMoPub
{
	public class AdBanner : AdvertBaseBanner
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegister:{0}:{1}",key,value);
			
			if (value)
			{
				IronSourceEvents.onBannerAdLoadedEvent += OnAdLoaded;
				IronSourceEvents.onBannerAdLoadFailedEvent += OnAdLoadFailed;
				
				IronSourceEvents.onBannerAdClickedEvent += OnAdClicked;
				IronSourceEvents.onBannerAdScreenPresentedEvent += OnAdScreenPresented;
				IronSourceEvents.onBannerAdLeftApplicationEvent += OnAdLeftApplication;
				IronSourceEvents.onBannerAdScreenDismissedEvent += OnAdScreenDismissed;
				
				OnRegisterBase();
				// if(IronSource.Agent.isInterstitialReady()) OnAdReady();
			}
			else
			{
				IronSourceEvents.onBannerAdLoadedEvent -= OnAdLoaded;
				IronSourceEvents.onBannerAdLoadFailedEvent -= OnAdLoadFailed;
				
				IronSourceEvents.onBannerAdClickedEvent -= OnAdClicked;
				IronSourceEvents.onBannerAdScreenPresentedEvent -= OnAdScreenPresented;
				IronSourceEvents.onBannerAdLeftApplicationEvent -= OnAdLeftApplication;
				IronSourceEvents.onBannerAdScreenDismissedEvent -= OnAdScreenDismissed;
			}
			return value;
		}
		#endregion
		
		
		#region Callback
		private void OnAdLoaded()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdLoaded");
			SetLoadedBase(true);
			IsDisplay = isDisplay;
		}
		
		private void OnAdLoadFailed(IronSourceError error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnAdLoadFailed:{0}",error.ToString());
			OnLoadFailBase();
		}
		
		private void OnAdClicked()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdClicked");
			OnClickBase();
		}
		
		private void OnAdScreenPresented()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdScreenPresented");
			OnShowBase();
		}
		
		private void OnAdLeftApplication()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdLeftApplication");
			OnVisitBase();
		}
		
		private void OnAdScreenDismissed()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnAdScreenDismissed");
			OnCloseBase();
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
			IronSource.Agent.loadBanner(xAdSize,xAdPosition);
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Show");
			IronSource.Agent.displayBanner();
			OnDisplay = true;
			OnWidth = width*multiplierPixel;
			OnHeight = height*multiplierPixel;
		}
		
		protected override void Hide()
		{
			IronSource.Agent.hideBanner();
			base.Hide();
		}
		#endif
		#endregion
		
		
		#region Custom
		private int width;
		private int height;
		private IronSourceBannerSize xAdSize
		{
			get
			{
				width = Mathf.FloorToInt(Screen.width/multiplierPixel);
				height = Mathf.FloorToInt(Screen.height/multiplierPixel);
				
				float heightInc = ExtScreen.HeightInc;
				IronSourceBannerSize returnObj;
				switch (adSize)
				{
					case "Banner":
					{
						width = 320;
						height = 50;
						returnObj = IronSourceBannerSize.BANNER;
						break;
					}
					case "Large":
					{
						width = 320;
						height = 90;
						returnObj = IronSourceBannerSize.LARGE;
						break;
					}
					case "Rectangle":
					{
						width = 300;
						height = 250;
						returnObj = IronSourceBannerSize.RECTANGLE;
						break;
					}
					case "Landscape":
					{
						if(heightInc>4.5f)
						{
							height = 90;
						}
						else if(heightInc>2.0f)
						{
							height = 50;
						}
						else
						{
							height = 32;
						}
						returnObj = new IronSourceBannerSize(width,height);
						break;
					}
					case "Portrait":
					{
						if(heightInc>5.0f)
						{
							height = 90;
						}
						else if(heightInc>3.0f)
						{
							height = 50;
						}
						else
						{
							height = 32;
						}
						returnObj = new IronSourceBannerSize(width,height);
						break;
					}
					default:
					{
						if(heightInc>4.5f)
						{
							width = 728;
							height = 90;
						}
						else if(heightInc>2.5f)
						{
							width = 320;
							height = 50;
						}
						else
						{
							width = 320;
							height = 50;
						}
						returnObj = IronSourceBannerSize.SMART;
						break;
					}
				}
				
				if(CanDebug)
				{
					Debug.LogFormat("Screen.currentResolution:{0}",Screen.currentResolution);
					Debug.LogFormat("Screen.width:{0}",Screen.width);
					Debug.LogFormat("Screen.height:{0}",Screen.height);
					Debug.LogFormat("Screen.dpi:{0}",Screen.dpi);
					Debug.LogFormat("ExtScreen.HeightInc:{0}",ExtScreen.HeightInc);
					Debug.LogFormat("ExtScreen.WidthInc:{0}",ExtScreen.WidthInc);
					Debug.LogFormat("Banner.width:{0}",width);
					Debug.LogFormat("Banner.height:{0}",height);
					Debug.LogFormat("DeviceDisplay.scaleFactor:{0}",DeviceDisplay.scaleFactor);
				}
				
				return returnObj;
			}
		}
		
		private IronSourceBannerPosition xAdPosition
		{
			get
			{
				switch (adPosition)
				{
					case "Top":
						return IronSourceBannerPosition.TOP;
					case "Bottom":
						return IronSourceBannerPosition.BOTTOM;
					default:
						return IronSourceBannerPosition.BOTTOM;
				}
			}
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xMoPub
{
	public class AdBanner : AdvertBase
	{
	}
}
#endif
#endif
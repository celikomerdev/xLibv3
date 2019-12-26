#if xLibv3
#if AdIronSource
using System;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.libAdvert.xIronSource
{
	public class AdBanner : AdvertBaseBanner
	{
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegister:{key}:{value}",this);
			
			if (value)
			{
				IronSourceEvents.onBannerAdLoadedEvent += OnAdLoaded;
				IronSourceEvents.onBannerAdLoadFailedEvent += OnAdLoadFailed;
				
				IronSourceEvents.onBannerAdClickedEvent += OnAdClicked;
				IronSourceEvents.onBannerAdScreenPresentedEvent += OnAdScreenPresented;
				IronSourceEvents.onBannerAdLeftApplicationEvent += OnAdLeftApplication;
				IronSourceEvents.onBannerAdScreenDismissedEvent += OnAdScreenDismissed;
				
				OnRegisterBase();
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
			DebugBanner();
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
		private int width = 0;
		private int height = 0;
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
					case "BANNER":
					{
						width = 320;
						height = 50;
						returnObj = IronSourceBannerSize.BANNER;
						break;
					}
					case "LARGE":
					{
						width = 320;
						height = 90;
						returnObj = IronSourceBannerSize.LARGE;
						break;
					}
					case "RECTANGLE":
					{
						width = 300;
						height = 250;
						returnObj = IronSourceBannerSize.RECTANGLE;
						break;
					}
					case "LANDSCAPE":
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
					case "PORTRAIT":
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
				
				return returnObj;
			}
		}
		
		// public enum IronSourceBannerPosition{BOTTOM,TOP}
		private IronSourceBannerPosition xAdPosition
		{
			get
			{
				IronSourceBannerPosition returnObj = IronSourceBannerPosition.BOTTOM;
				Enum.TryParse(adPosition,out IronSourceBannerPosition myStatus);
				return returnObj;
			}
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xIronSource
{
	public class AdBanner : AdvertBaseBanner
	{
	}
}
#endif
#endif
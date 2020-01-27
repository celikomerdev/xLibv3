#if xLibv3
#if AdIronSource
using UnityEngine;

namespace xLib.libAdvert.xIronSource
{
	public class AdBanner : AdvertBaseBanner
	{
		#region TryRegister
		protected override bool TryRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:TryRegister:{key}:{register}",this);
			
			if (register)
			{
				IronSourceEvents.onBannerAdLoadFailedEvent += onBannerAdLoadFailedEvent;
				IronSourceEvents.onBannerAdLoadedEvent += onBannerAdLoadedEvent;
				
				IronSourceEvents.onBannerAdClickedEvent += onBannerAdClickedEvent;
				IronSourceEvents.onBannerAdScreenPresentedEvent += onBannerAdScreenPresentedEvent;
				
				IronSourceEvents.onBannerAdLeftApplicationEvent += onBannerAdLeftApplicationEvent;
				IronSourceEvents.onBannerAdScreenDismissedEvent += onBannerAdScreenDismissedEvent;
				
				OnRegisteredBase();
				if(MnIronSource.hasBanner) onBannerAdLoadedEvent();
			}
			else
			{
				IronSourceEvents.onBannerAdLoadFailedEvent -= onBannerAdLoadFailedEvent;
				IronSourceEvents.onBannerAdLoadedEvent -= onBannerAdLoadedEvent;
				
				IronSourceEvents.onBannerAdClickedEvent -= onBannerAdClickedEvent;
				IronSourceEvents.onBannerAdScreenPresentedEvent -= onBannerAdScreenPresentedEvent;
				
				
				IronSourceEvents.onBannerAdLeftApplicationEvent -= onBannerAdLeftApplicationEvent;
				IronSourceEvents.onBannerAdScreenDismissedEvent -= onBannerAdScreenDismissedEvent;
			}
			return register;
		}
		#endregion
		
		
		#region Callback
		//Invoked when the banner loading process has failed.
		private void onBannerAdLoadFailedEvent(IronSourceError error)
		{
			xLogger.LogException($"{this.name}:onBannerAdLoadFailedEvent:{error.ToString()}",this);
			OnLoadFailBase();
		}
		
		//Invoked once the banner has loaded
		private void onBannerAdLoadedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onBannerAdLoadedEvent",this);
			SetLoadedBase(true);
			IsDisplay = isDisplay;
		}
		
		//Invoked when end user clicks on the banner ad
		private void onBannerAdClickedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onBannerAdClickedEvent",this);
			OnClickBase();
		}
		
		//Notifies the presentation of a full screen content following user click
		private void onBannerAdScreenPresentedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onBannerAdScreenPresentedEvent",this);
			OnShowBase();
		}
		
		//Invoked when the user leaves the app
		private void onBannerAdLeftApplicationEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onBannerAdLeftApplicationEvent",this);
			OnVisitBase();
		}
		
		//Notifies the presented screen has been dismissed
		private void onBannerAdScreenDismissedEvent()
		{
			if(CanDebug) Debug.Log($"{this.name}:onBannerAdScreenDismissedEvent",this);
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
			if(CanDebug) Debug.Log($"{this.name}:Load",this);
			IronSource.Agent.loadBanner(xAdSize,xAdPosition);
			DebugBanner();
		}
		
		protected override void Show()
		{
			if(CanDebug) Debug.Log($"{this.name}:Show",this);
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
		private IronSourceBannerPosition xAdPosition
		{
			get
			{
				IronSourceBannerPosition returnObj = IronSourceBannerPosition.BOTTOM;
				System.Enum.TryParse(adPosition,out IronSourceBannerPosition myStatus);
				return returnObj;
			}
		}
		// public enum IronSourceBannerPosition
		// {
		// 	TOP = 1,
		// 	BOTTOM = 2
		// }
		
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
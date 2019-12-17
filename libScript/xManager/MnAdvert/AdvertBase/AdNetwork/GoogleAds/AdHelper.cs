#if xLibv3
#if AdGoogle
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace xLib.libAdvert.xGoogleAds
{
	public static class AdHelper
	{
		public static float SmartBannerHeight()
		{
			float dpi = Mathf.RoundToInt(Screen.dpi/160);
			//float dpi = Screen.dpi/160;
			
			if (Screen.height <= dpi*400) return dpi*32;
			if (Screen.height <= dpi*720) return dpi*50;
			return dpi*90;
		}
		
		public static float GetHeightInScreen(this BannerView advert)
		{
			return advert.GetHeightInPixels()*(Screen.dpi/160);
		}
		
		public static float GetWidthInScreen(this BannerView advert)
		{
			return advert.GetWidthInPixels()*(Screen.dpi/160);
		}
	}
}
#endif
#endif
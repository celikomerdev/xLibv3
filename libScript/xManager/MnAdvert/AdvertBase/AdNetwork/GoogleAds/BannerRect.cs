#if xLibv3
#if AdGoogle
using UnityEngine;
using UnityEngine.UI;
using xLib.libAdvert.xGoogleAds;

namespace xLib.libAdvert.xGoogleAds
{
	public class BannerRect : BaseM
	{
		public CanvasScaler canvas;
		public RectTransform rect;
		public bool scale;
		public float canvasScale;
		public bool height;
		
		private void OnEnable()
		{
			Vector2 size = rect.sizeDelta;
			size.y = AdHelper.SmartBannerHeight();
			
			if(scale)
			{
				if(height)
				{
					canvasScale = 1/(Screen.height/canvas.referenceResolution.y);
				}
				else
				{
					canvasScale = 1/(Screen.width/canvas.referenceResolution.x);
				}
				
				size.y *= canvasScale;
			}
			
			rect.sizeDelta = size;
		}
	}
}
#endif
#endif
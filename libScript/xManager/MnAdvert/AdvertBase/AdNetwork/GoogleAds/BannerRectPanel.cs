#if xLibv3
#if AdGoogle
using UnityEngine;
using UnityEngine.UI;
using xLib.libAdvert.xGoogleAds;

namespace xLib.libAdvert.xGoogleAds
{
	public class BannerRectPanel : BaseM
	{
		public CanvasScaler canvas;
		public RectTransform rect;
		public bool scale;
		
		public bool top;
		public bool bottom;
		public float canvasScale;
		public bool height;
		
		
		public void Bottom(bool value)
		{
			bottom = value;
			Refresh();
		}
		
		public void Top(bool value)
		{
			top = value;
			Refresh();
		}
		
		private void Refresh()
		{
			Vector2 offsetMin = Vector2.zero;
			Vector3 offsetMax = Vector2.zero;
			
			if(bottom) offsetMin.y = AdHelper.SmartBannerHeight();
			if(top) offsetMax.y = -AdHelper.SmartBannerHeight();
			
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
				
				offsetMin *= canvasScale;
				offsetMax *= canvasScale;
			}
			
			rect.offsetMin = offsetMin;
			rect.offsetMax = offsetMax;
		}
	}
}
#endif
#endif
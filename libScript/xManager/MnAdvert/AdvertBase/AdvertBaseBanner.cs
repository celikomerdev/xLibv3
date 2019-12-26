﻿#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.libAdvert
{
	public abstract class AdvertBaseBanner : AdvertBase
	{
		[SerializeField]protected string adSize;
		[SerializeField]protected string adPosition;
		
		protected override void Show()
		{
			DebugBanner();
			OnDisplay = true;
			OnWidth = width;
			OnHeight = height;
		}
		
		protected virtual void Hide()
		{
			OnDisplay = false;
			OnWidth = 0;
			OnHeight = 0;
		}
		
		public void HideBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:HideBase",this);
			if(!isLoad) return;
			Hide();
		}
		
		protected bool isDisplay;
		public bool IsDisplay
		{
			set
			{
				isDisplay = value;
				if(value)
				{
					LoadBase();
					ShowBase();
				}
				else HideBase();
			}
		}
		
		[SerializeField]private EventBool onDisplay = new EventBool();
		protected bool OnDisplay
		{
			set
			{
				onDisplay.Invoke(value);
			}
		}
		
		
		[SerializeField]private EventFloat onDisplayWidth = new EventFloat();
		protected int width = 90;
		protected float OnWidth
		{
			set
			{
				if(CanDebug) Debug.Log($"{this.name}:OnWidth:{value}",this);
				onDisplayWidth.Invoke(value);
			}
		}
		
		[SerializeField]private EventFloat onDisplayHeight = new EventFloat();
		protected int height = 90;
		protected float OnHeight
		{
			set
			{
				if(CanDebug) Debug.Log($"{this.name}:OnHeight:{value}",this);
				onDisplayHeight.Invoke(value);
			}
		}
		
		protected static float multiplierPixel
		{
			get
			{
				#if UNITY_ANDROID
				return ExtScreen.dpi/160f;
				#elif UNITY_IOS
				return DeviceDisplay.scaleFactor;
				#else
				return ExtScreen.dpi/160f;
				#endif
			}
		}
		
		protected void DebugBanner()
		{
			if(CanDebug)
			{
				Debug.Log($"{this.name}:Screen.currentResolution:{Screen.currentResolution}",this);
				Debug.Log($"{this.name}:Screen.width:{Screen.width}",this);
				Debug.Log($"{this.name}:Screen.height:{Screen.height}",this);
				Debug.Log($"{this.name}:Screen.dpi:{Screen.dpi}",this);
				Debug.Log($"{this.name}:ExtScreen.HeightInc:{ExtScreen.HeightInc}",this);
				Debug.Log($"{this.name}:ExtScreen.WidthInc:{ExtScreen.WidthInc}",this);
				Debug.Log($"{this.name}:Banner.width:{width}",this);
				Debug.Log($"{this.name}:Banner.height:{height}",this);
				Debug.Log($"{this.name}:DeviceDisplay.scaleFactor:{DeviceDisplay.scaleFactor}",this);
			}
		}
	}
}
#endif
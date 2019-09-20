#if xLibv3
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
			OnDisplay = true;
			OnWidth = 90;
			OnHeight = 90;
		}
		
		protected virtual void Hide()
		{
			OnDisplay = false;
			OnWidth = 0;
			OnHeight = 0;
		}
		public void HideBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":HideBase");
			if(!isLoad) return;
			Hide();
		}
		
		protected bool isDisplay;
		public bool IsDisplay
		{
			set
			{
				isDisplay = value;
				if(value) ShowBase();
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
		protected float OnWidth
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":OnWidth:{0}",value);
				onDisplayWidth.Invoke(value);
			}
		}
		
		[SerializeField]private EventFloat onDisplayHeight = new EventFloat();
		protected float OnHeight
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":OnHeight:{0}",value);
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
	}
}
#endif
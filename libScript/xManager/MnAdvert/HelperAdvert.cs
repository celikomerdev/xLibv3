#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.libAdvert
{
	public class HelperAdvert : BaseMainM
	{
		[SerializeField]private bool canMessage = false;
		
		#region Interval
		[SerializeField]private float lastTime = 0;
		[SerializeField]private int intervalTime = 0;
		public int IntervalTime
		{
			set
			{
				intervalTime = value;
			}
		}
		
		private bool TimeReady
		{
			get
			{
				if(intervalTime==-1)
				{
					MessageNotLoaded();
					return false;
				}
				
				float remainingTime = (lastTime+intervalTime)-Time.realtimeSinceStartup;
				if(remainingTime>0)
				{
					if(canMessage)
					{
						StPopupBar.QueueMessage(string.Format(MnLocalize.GetValue("Please Wait {0} Seconds"),remainingTime.ToString("F0")));
					}
					return false;
				}
				return true;
			}
		}
		
		private void MessageNotLoaded()
		{
			if(canMessage) StPopupBar.QueueMessage("Downloading Ads");
		}
		#endregion
		
		
		#region Public
		public void TryShowInterstitial()
		{
			if(MnAdvert.ins.inShow.Value) return;
			if(!TimeReady) return;
			
			#if UNITY_EDITOR
			if(Application.isEditor)
			{
				Register();
				MnCoroutine.WaitForSeconds(delay:3.0f,call:OnClose);
				return;
			}
			#endif
			
			MnAdvert.ins.interstitial.LoadBase();
			if(!MnAdvert.ins.interstitial.isLoad)
			{
				MessageNotLoaded();
				return;
			}
			
			Register();
			MnCoroutine.WaitForSeconds(delay:0.5f,call:MnAdvert.ins.interstitial.ShowBase);
		}
		
		public void TryShowRewarded()
		{
			if(MnAdvert.ins.inShow.Value) return;
			if(!TimeReady) return;
			
			#if UNITY_EDITOR
			if(Application.isEditor)
			{
				Register();
				MnCoroutine.WaitForSeconds(delay:3.0f,call:delegate{OnReward(1);});
				return;
			}
			#endif
			
			MnAdvert.ins.rewarded.LoadBase();
			if(!MnAdvert.ins.rewarded.isLoad)
			{
				MessageNotLoaded();
				return;
			}
			
			Register();
			MnCoroutine.WaitForSeconds(delay:0.5f,call:MnAdvert.ins.rewarded.ShowBase);
		}
		#endregion
		
		
		
		#region Callback
		private void Register()
		{
			MnAdvert.ins.inShow.Value = true;
			lastTime = Time.realtimeSinceStartup;
			MnAdvert.ins.onShow.eventUnity.AddListener(OnShow);
			MnAdvert.ins.onClose.eventUnity.AddListener(OnClose);
			MnAdvert.ins.onReward.eventInt.AddListener(OnReward);
		}
		
		public EventUnity onShow;
		private void OnShow()
		{
			MnAdvert.ins.inShow.Value = true;
			lastTime = Time.realtimeSinceStartup;
			onShow.Invoke();
		}
		
		public EventUnity onClose;
		private void OnClose()
		{
			MnAdvert.ins.inShow.Value = false;
			lastTime = Time.realtimeSinceStartup;
			onClose.Invoke();
		}
		
		public EventInt onReward;
		private void OnReward(int value)
		{
			MnAdvert.ins.inShow.Value = false;
			lastTime = Time.realtimeSinceStartup;
			onReward.Invoke(value);
		}
		#endregion
	}
}
#endif
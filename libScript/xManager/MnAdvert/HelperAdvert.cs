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
			if(!TimeReady) return;
			
			MnAdvert.ins.interstitial.LoadBase();
			if(!MnAdvert.ins.interstitial.IsLoad)
			{
				MessageNotLoaded();
				return;
			}
			
			if(MnAdvert.ins.inShow.Value) return;
			MnAdvert.ins.inShow.Value = true;
			
			RegisterListeners(true);
			MnCoroutine.ins.WaitForSeconds(delay:0.5f,call:MnAdvert.ins.interstitial.ShowBase);
		}
		
		public void TryShowRewarded()
		{
			if(!TimeReady) return;
			
			MnAdvert.ins.rewarded.LoadBase();
			if(!MnAdvert.ins.rewarded.IsLoad)
			{
				MessageNotLoaded();
				return;
			}
			
			if(MnAdvert.ins.inShow.Value) return;
			MnAdvert.ins.inShow.Value = true;
			
			RegisterListeners(true);
			MnCoroutine.ins.WaitForSeconds(delay:0.5f,call:MnAdvert.ins.rewarded.ShowBase);
		}
		#endregion
		
		
		
		#region Callback
		private void RegisterListeners(bool value)
		{
			lastTime = Time.realtimeSinceStartup;
			MnAdvert.ins.RemoveAllListeners();
			if(value)
			{
				MnAdvert.ins.onShow.eventUnity.AddListener(OnShow);
				MnAdvert.ins.onReward.eventInt.AddListener(OnReward);
				MnAdvert.ins.onClose.eventUnity.AddListener(OnClose);
			}
			else
			{
				MnAdvert.ins.onShow.eventUnity.RemoveListener(OnShow);
				MnAdvert.ins.onReward.eventInt.RemoveListener(OnReward);
				MnAdvert.ins.onClose.eventUnity.RemoveListener(OnClose);
			}
		}
		
		public EventUnity onShow = new EventUnity();
		private void OnShow()
		{
			onShow.Invoke();
		}
		
		public EventInt onReward = new EventInt();
		private void OnReward(int value)
		{
			MnCoroutine.ins.WaitForSeconds(delay:0.5f,call:delegate{onReward.Invoke(value);});
		}
		
		public EventUnity onClose = new EventUnity();
		private void OnClose()
		{
			MnCoroutine.ins.WaitForSeconds(delay:1f,call:delegate
			{
				onClose.Invoke();
				RegisterListeners(false);
			});
		}
		#endregion
	}
}
#endif
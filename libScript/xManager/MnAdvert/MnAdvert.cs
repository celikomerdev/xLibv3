#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.libAdvert;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnAdvert : SingletonM<MnAdvert>
	{
		public NodeBool inShow;
		public AdvertBase interstitial;
		public AdvertBase rewarded;
		
		public bool privacyAccepted = true;
		public bool ageRestiction = false;
		
		public int age;
		public string gender;
		public bool isPaying;
		
		public string[] keyword;
		
		
		#region Mono
		protected override void Started()
		{
			StAdvert.Init();
			Register(true);
		}
		
		protected override void OnDestroyed()
		{
			Register(false);
		}
		#endregion
		
		
		#region Register
		private void Register(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Register:{0}",value);
			
			if(value)
			{
				interstitial.onShow.eventUnity.AddListener(OnShow);
				interstitial.onClose.eventUnity.AddListener(OnClose);
				
				rewarded.onShow.eventUnity.AddListener(OnShow);
				rewarded.onClose.eventUnity.AddListener(OnClose);
				rewarded.onReward.eventInt.AddListener(OnReward);
			}
			else
			{
				interstitial.onShow.eventUnity.RemoveListener(OnShow);
				interstitial.onClose.eventUnity.RemoveListener(OnClose);
				
				rewarded.onShow.eventUnity.RemoveListener(OnShow);
				rewarded.onClose.eventUnity.RemoveListener(OnClose);
				rewarded.onReward.eventInt.RemoveListener(OnReward);
			}
		}
		#endregion
		
		
		#region Callback
		public EventUnity onShow;
		public void OnShow()
		{
			onShow.Invoke();
		}
		
		public EventUnity onClose;
		public void OnClose()
		{
			onClose.Invoke();
		}
		
		public EventInt onReward;
		public void OnReward(int value)
		{
			onReward.Invoke(value);
		}
		#endregion
	}
}
#endif
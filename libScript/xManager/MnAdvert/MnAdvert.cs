#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.libAdvert;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnAdvert : SingletonM<MnAdvert>
	{
		public NodeBool inShow = null;
		public AdvertBase interstitial = null;
		public AdvertBase rewarded = null;
		
		public bool privacyAccepted = true;
		public bool ageRestiction = false;
		
		public int age = 25;
		public string gender = "";
		public bool isPaying = true;
		
		public string[] keyword = new string[0];
		
		
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
		public EventUnity onShow = new EventUnity();
		public void OnShow()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnShow");
			inShow.Value = true;
			onShow.Invoke();
		}
		
		public EventUnity onClose = new EventUnity();
		public void OnClose()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnClose");
			inShow.Value = false;
			onClose.Invoke();
		}
		
		public EventInt onReward = new EventInt();
		public void OnReward(int value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnReward:{0}",value);
			#if UNITY_EDITOR
			if(!inShow.Value) return;
			#endif
			inShow.Value = false;
			onReward.Invoke(value);
		}
		
		public void RemoveAllListeners()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":RemoveAllListeners");
			onShow.eventUnity.RemoveAllListeners();
			onClose.eventUnity.RemoveAllListeners();
			onReward.eventInt.RemoveAllListeners();
		}
		#endregion
	}
}
#endif
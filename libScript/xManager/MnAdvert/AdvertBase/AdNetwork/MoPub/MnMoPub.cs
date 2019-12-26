#if xLibv3
#if AdMoPub
using UnityEngine;

namespace xLib.xMoPub
{
	public class MnMoPub : BaseWorkM
	{
		[SerializeField]private string key;
		
		#region Mono
		private void Awake()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Awake");
			Init();
		}
		
		private void OnApplicationPause(bool value)
		{
			IronSource.Agent.onApplicationPause(value);
		}
		#endregion
		
		
		#region Init
		private void Init()
		{
			IronSource.Agent.setAdaptersDebug(CanDebug);
			IronSource.Agent.shouldTrackNetworkState(true);
			FillUser();
			
			IronSource.Agent.init(MnKey.GetValue(key),IronSourceAdUnits.REWARDED_VIDEO,IronSourceAdUnits.INTERSTITIAL,IronSourceAdUnits.OFFERWALL,IronSourceAdUnits.BANNER);
			if(CanDebug) IronSource.Agent.validateIntegration();
		}
		
		private void FillUser()
		{
			IronSource.Agent.setConsent(MnAdvert.ins.privacyAccepted);
			// IronSource.Agent.setUserId(SystemInfo.deviceUniqueIdentifier);
			
			// if(MnAdvert.ins.age>0) IronSource.Agent.setAge(MnAdvert.ins.age);
			// if(!string.IsNullOrEmpty(MnAdvert.ins.gender)) IronSource.Agent.setGender(MnAdvert.ins.gender);
			
			// IronSourceSegment mIronSegment = new IronSourceSegment();
			// if(MnAdvert.ins.isPaying) mIronSegment.isPaying = 1;
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib.xMoPub
{
	public class MnMoPub : BaseWorkM
	{
		[SerializeField]private string key;
	}
}
#endif
#endif
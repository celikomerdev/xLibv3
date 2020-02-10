#if xLibv3
#if AdGoogle
using UnityEngine;
using GoogleMobileAds.Api;

#region Mediation

#if MedAdColony
using GoogleMobileAds.Api.Mediation.AdColony;
#endif

#if MedAppLovin
using GoogleMobileAds.Api.Mediation.AppLovin;
#endif

#if MedChartboost
using GoogleMobileAds.Api.Mediation.Chartboost;
#endif

#if MedInMobi
using GoogleMobileAds.Api.Mediation.InMobi;
#endif

#if MedMyTarget
using GoogleMobileAds.Api.Mediation.MyTarget;
#endif

#if MedUnityAds
using GoogleMobileAds.Api.Mediation.UnityAds;
#endif

#if MedVungle
using GoogleMobileAds.Api.Mediation.Vungle;
#endif

#endregion

namespace xLib.libAdvert.xGoogleAds
{
	public class MnGoogleAds : SingletonM<MnGoogleAds>
	{
		public string key;
		
		#region Mono
		protected override void Started()
		{
			Init();
		}
		
		public override void Init()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Init");
			
			MobileAds.Initialize(MnKey.GetValue(key));
			
			#region Mediation
			#if MedAppLovin
			AppLovin.Initialize();
			AppLovin.SetHasUserConsent(MnAdvert.ins.privacyAccepted);
			AppLovin.SetIsAgeRestrictedUser(MnAdvert.ins.ageRestiction);
			#endif
			
			#if MedChartboost
			Chartboost.RestrictDataCollection(!MnAdvert.ins.privacyAccepted);
			#endif
			
			#if MedInMobi
			Dictionary<string, string> inMobiConsent = new Dictionary<string, string>();
			inMobiConsent.Add("gdpr_consent_available", "true");
			inMobiConsent.Add("gdpr", "1");
			InMobi.UpdateGDPRConsent(inMobiConsent);
			#endif
			
			#if MedMyTarget
			MyTarget.SetUserConsent(MnAdvert.ins.privacyAccepted);
			MyTarget.SetUserAgeRestricted(MnAdvert.ins.ageRestiction);
			#endif
			
			#if MedUnityAds
			UnityAds.SetGDPRConsentMetaData(MnAdvert.ins.privacyAccepted);
			#endif
			#endregion
		}
		#endregion
		
		
		#region Request
		public AdRequest.Builder Builder()
		{
			#region Builder
			AdRequest.Builder builder = new AdRequest.Builder();
			
			builder.TagForChildDirectedTreatment(MnAdvert.ins.ageRestiction);
			
			for (int i = 0; i < MnAdvert.ins.keyword.Length; i++)
			{
				builder.AddKeyword(MnAdvert.ins.keyword[i]);
			}
			
			#if CanDebug
			builder.AddTestDevice(StAdvert.TestDeviceID);
			#endif
			#endregion
			
			
			#if MedAdColony
			AdColonyMediationExtras extrasAdColony = new AdColonyMediationExtras();
			extrasAdColony.SetGDPRRequired(true);
			extrasAdColony.SetGDPRConsentString("1");
			extrasAdColony.SetUserId("user_id");
			extrasAdColony.SetZoneId("zone_id");
			extrasAdColony.SetShowPrePopup(true);
			extrasAdColony.SetShowPostPopup(true);
			extrasAdColony.SetTestMode(true);
			builder.AddMediationExtras(extrasAdColony);
			#endif
			
			return builder;
		}
		#endregion
	}
}
#else
namespace xLib.libAdvert.xGoogleAds
{
	public class MnGoogleAds : SingletonM<MnGoogleAds>
	{
		public string key;
	}
}
#endif
#endif
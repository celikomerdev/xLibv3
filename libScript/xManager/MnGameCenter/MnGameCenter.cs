#if xLibv3
#if GameCenter
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace xLib
{
	public class MnGameCenter : SingletonM<MnGameCenter>
	{
		[SerializeField]private bool showBanner = true;
		
		#region Mono
		protected override void OnEnabled()
		{
			MnSocial.ins.eventInit.eventUnity.AddListener(Init);
			MnSocial.loginOut += LoginOut;
			MnSocial.ins.eventShowLeaderboard.eventString.AddListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.AddListener(ShowAchievementsUI);
		}
		
		protected override void OnDisabled()
		{
			MnSocial.ins.eventInit.eventUnity.RemoveListener(Init);
			MnSocial.loginOut -= LoginOut;
			MnSocial.ins.eventShowLeaderboard.eventString.RemoveListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.RemoveListener(ShowAchievementsUI);
		}
		#endregion
		
		public override void Init()
		{
			base.Init();
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(showBanner);
		}
		
		private void LoginOut(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:LoginOut:{value}",this);
			if(!value)
			{
				if(CanDebug) Debug.LogWarnin($"Logout:NotSupported",this);
				return;
			}
			MnSocial.ins.inLogin.Value = true;
			Social.localUser.Authenticate(IsLogin);
		}
		
		private void IsLogin(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:IsLogin:{value}",this);
			
			if(value)
			{
				MnSocial.ins.userID.Value = Social.localUser.id;
				MnSocial.ins.personName.Value = Social.localUser.userName;
				MnSocial.ins.personPicture.Value = Social.localUser.image;
			}
			else
			{
				MnSocial.ins.userID.Value = userID.ValueDefault;
				MnSocial.ins.personName.Value = personName.ValueDefault;
				MnSocial.ins.personPicture.Value = personPicture.ValueDefault;
			}
			
			MnSocial.ins.isLogin.Value = value;
			MnSocial.ins.isSilent.Value = value;
			MnSocial.ins.inLogin.Value = false;
		}
		
		private void ReportScore(string id,long value)
		{
			if(CanDebug) Debug.Log($"{this.name}:ReportScore:{id}:{value}",this);
			Social.ReportScore(id,value,(result)=>
			{
				if(CanDebug) Debug.Log($"{this.name}:ReportScore:{id}:{value}:{result}",this);
			});
		}
		
		private void ReportProgress(string id,double value)
		{
			if(CanDebug) Debug.Log($"{this.name}:ReportProgress:{id}:{value}",this);
			Social.ReportProgress(id,value,(result)=>
			{
				if(CanDebug) Debug.Log($"{this.name}:ReportProgress:{id}:{value}:{result}",this);
			});
		}
		
		private void ShowLeaderboardUI(string id)
		{
			GameCenterPlatform.ShowLeaderboardUI(id,UnityEngine.SocialPlatforms.TimeScope.AllTime);
		}
		
		private void ShowAchievementsUI(string id)
		{
			Social.ShowAchievementsUI();
		}
	}
}
#else
using UnityEngine;

namespace xLib
{
	public class MnGameCenter : SingletonM<MnGameCenter>
	{
		#pragma warning disable
		[SerializeField]private bool showBanner = true;
		#pragma warning restore
		
		public void ShowLeaderboardUI(string id){}
		public void ShowAchievementsUI(string id){}
	}
}
#endif
#endif
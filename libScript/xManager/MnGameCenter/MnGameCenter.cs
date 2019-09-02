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
		protected override void Awaked()
		{
			MnSocial.ins.eventInit.eventUnity.AddListener(Init);
			MnSocial.ins.eventShowLeaderboard.eventString.AddListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.AddListener(ShowAchievementsUI);
		}
		
		protected override void OnDestroyed()
		{
			MnSocial.ins.eventInit.eventUnity.RemoveListener(Init);
			MnSocial.ins.eventShowLeaderboard.eventString.RemoveListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.RemoveListener(ShowAchievementsUI);
		}
		#endregion
		
		#region Init
		public override void Init()
		{
			base.Init();
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(showBanner);
		}
		#endregion
		
		#region Public
		public void ShowLeaderboardUI(string id)
		{
			GameCenterPlatform.ShowLeaderboardUI(id,UnityEngine.SocialPlatforms.TimeScope.AllTime);
		}
		
		public void ShowAchievementsUI(string id)
		{
			Social.ShowAchievementsUI();
		}
		#endregion
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
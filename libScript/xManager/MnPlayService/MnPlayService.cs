#if xLibv3
#if GooglePlayService
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnPlayService : SingletonM<MnPlayService>
	{
		[SerializeField]private bool enableHidePopups = false;
		[SerializeField]private bool savedGames = false;
		[SerializeField]private bool requestIdToken = false;
		[SerializeField]private bool requestServerAuthCode = false;
		[SerializeField]private bool requestEmail = false;
		
		[SerializeField]private NodeString displayName = null;
		[SerializeField]private NodeString idToken = null;
		[SerializeField]private NodeString authCode = null;
		
		#region Mono
		protected override void Awaked()
		{
			MnSocial.ins.eventInit.eventUnity.AddListener(Init);
			MnSocial.ins.isLogin.Listener(true,OnLogin,true);
			MnSocial.ins.eventShowLeaderboard.eventString.AddListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.AddListener(ShowAchievementsUI);
		}
		
		protected override void OnDestroyed()
		{
			MnSocial.ins.eventInit.eventUnity.RemoveListener(Init);
			MnSocial.ins.isLogin.Listener(false,OnLogin,true);
			MnSocial.ins.eventShowLeaderboard.eventString.RemoveListener(ShowLeaderboardUI);
			MnSocial.ins.eventShowAchievement.eventString.RemoveListener(ShowAchievementsUI);
		}
		#endregion
		
		#region Init
		public override void Init()
		{
			base.Init();
			PlayGamesPlatform.DebugLogEnabled = CanDebug;
			
			PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
			builder.WithInvitationDelegate(OnInvitationReceive);
			builder.WithMatchDelegate(OnTurnMatchGot);
			builder.RequestServerAuthCode(requestServerAuthCode);
			
			if(enableHidePopups) builder.EnableHidePopups();
			if(savedGames) builder.EnableSavedGames();
			if(requestIdToken) builder.RequestIdToken();
			if(requestEmail) builder.RequestEmail();
			
			PlayGamesClientConfiguration config = builder.Build();
			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.Activate();
		}
		#endregion
		
		#region Tokens
		public void OnLogin(bool value)
		{
			if(value)
			{
				displayName.Value = PlayGamesPlatform.Instance.GetUserDisplayName();
				if(requestIdToken) idToken.Value = PlayGamesPlatform.Instance.GetIdToken();
				if(requestServerAuthCode) authCode.Value = PlayGamesPlatform.Instance.GetServerAuthCode();
			}
			else
			{
				displayName.Value = "Guest";
				idToken.Value = "";
				authCode.Value = "";
			}
		}
		#endregion
		
		
		#region Leaderboard/Achievement
		public void ShowLeaderboardUI(string id)
		{
			PlayGamesPlatform.Instance.ShowLeaderboardUI(id);
		}
		
		public void ShowAchievementsUI(string id)
		{
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		#endregion
		
		
		#region Invitation
		public Invitation invitation;
		public UnityAction onInvitationReceive = delegate(){};
		private void OnInvitationReceive(Invitation _invitation, bool _shouldAutoAccept)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": OnInvitationReceive:{0}",_shouldAutoAccept);
			invitation = _invitation;
			onInvitationReceive.Invoke();
			if (_shouldAutoAccept) OnInvitationAccept();
		}
		
		public UnityAction onInvitationAccept = delegate(){};
		private void OnInvitationAccept()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": OnInvitationAccept");
			onInvitationAccept.Invoke();
		}
		
		//PlayGamesPlatform.Instance.TurnBased.DeclineInvitation(mIncomingInvitation.InvitationId);
		public UnityAction onInvitationDecline = delegate(){};
		public void OnInvitationDecline()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": OnInvitationDecline");
			onInvitationDecline.Invoke();
		}
		#endregion
		
		
		#region Match
		public UnityAction onTurnMatchGot = delegate(){};
		private void OnTurnMatchGot(TurnBasedMatch _match, bool _shouldAutoLaunch)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": OnTurnMatchGot:{0}",_shouldAutoLaunch);
			onTurnMatchGot.Invoke();
			// MnMatchTurn.ins.shouldAutoLaunch = _shouldAutoLaunch;
			// MnMatchTurn.ins.match = _match;
		}
		#endregion
	}
}
#else
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnPlayService : SingletonM<MnPlayService>
	{
		#pragma warning disable
		[SerializeField]private bool enableHidePopups;
		[SerializeField]private bool savedGames;
		[SerializeField]private bool requestIdToken;
		[SerializeField]private bool requestServerAuthCode;
		[SerializeField]private bool requestEmail;
		
		[SerializeField]private NodeString displayName;
		[SerializeField]private NodeString idToken;
		[SerializeField]private NodeString authCode;
		
		public void OnLogin(bool value){}
		public void ShowLeaderboardUI(string id){}
		public void ShowAchievementsUI(string id){}
		#pragma warning restore
	}
}
#endif
#endif
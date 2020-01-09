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
		[SerializeField]private bool requestEmail = false;
		[SerializeField]private bool requestServerAuthCode = false;
		
		[SerializeField]private NodeString displayName = null;
		[SerializeField]private NodeString idToken = null;
		[SerializeField]private NodeString authCode = null;
		
		
		#region Mono
		protected override void OnEnabled()
		{
			// MnSocial.actionInit += Init;
			MnSocial.actionLoginOut += LoginOut;
			
			MnSocial.actionShowLeaderboard += ShowLeaderboard;
			MnSocial.actionReportLeaderboard += ReportLeaderboard;
			
			MnSocial.actionShowAchievement += ShowAchievement;
			MnSocial.actionReportAchievement += ReportAchievement;
		}
		
		protected override void OnDisabled()
		{
			// MnSocial.actionInit -= Init;
			MnSocial.actionLoginOut -= LoginOut;
			
			MnSocial.actionShowLeaderboard -= ShowLeaderboard;
			MnSocial.actionReportLeaderboard -= ReportLeaderboard;
			
			MnSocial.actionShowAchievement -= ShowAchievement;
			MnSocial.actionReportAchievement -= ReportAchievement;
		}
		#endregion
		
		
		#region Init
		private bool inInit = false;
		private bool isInit = false;
		protected override void Inited()
		{
			if(inInit) return;
			if(isInit) return;
			inInit = true;
			
			MnThread.StartThread(useThread:true,priority:1,context:this,call:delegate
			{
				PlayGamesPlatform.DebugLogEnabled = CanDebug;
			
				PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
				builder.WithInvitationDelegate(OnInvitationReceive);
				builder.WithMatchDelegate(OnTurnMatchGot);
				
				if(enableHidePopups) builder.EnableHidePopups();
				if(savedGames) builder.EnableSavedGames();
				if(requestIdToken) builder.RequestIdToken();
				if(requestEmail) builder.RequestEmail();
				if(requestServerAuthCode) builder.RequestServerAuthCode(requestServerAuthCode);
				
				PlayGamesClientConfiguration config = builder.Build();
				PlayGamesPlatform.InitializeInstance(config);
				
				inInit = false;
				isInit = true;
				MnThread.Register(context:this,call:delegate{LoginOut(true);});
			});
		}
		#endregion
		
		
		#region LoginOut
		private void LoginOut(bool value)
		{
			Init();
			if(!isInit) return;
			if(CanDebug) Debug.Log($"{this.name}:LoginOut:{value}",this);
			
			if(value)
			{
				MnSocial.ins.inLogin.Value = true;
				MnThread.StartThread(useThread:false,priority:1,context:this,call:delegate{PlayGamesPlatform.Instance.Authenticate(IsLogin);});
			}
			else
			{
				MnThread.StartThread(useThread:false,priority:1,context:this,call:delegate{PlayGamesPlatform.Instance.SignOut();});
				IsLogin(false);
			}
		}
		
		private void IsLogin(bool value)
		{
			MnThread.Register(context:this,call:delegate
			{
				if(CanDebug) Debug.Log($"{this.name}:IsLogin:{value}",this);
				
				if(value)
				{
					// StartCoroutine(DownloadImage(PlayGamesPlatform.Instance.GetUserImageUrl())); //TODO
					displayName.Value = PlayGamesPlatform.Instance.GetUserDisplayName();
					if(requestIdToken) idToken.Value = PlayGamesPlatform.Instance.GetIdToken();
					if(requestServerAuthCode) authCode.Value = PlayGamesPlatform.Instance.GetServerAuthCode();
				}
				else
				{
					displayName.ValueDefaultReset();
					idToken.ValueDefaultReset();
					authCode.ValueDefaultReset();
				}
				
				MnSocial.ins.isLogin.Value = value;
				MnSocial.ins.isSilent.Value = value;
				MnSocial.ins.inLogin.Value = false;
			});
		}
		#endregion
		
		
		#region Leaderboard/Achievement
		private void ShowLeaderboard(string key)
		{
			string idPlatform = MnKey.GetValue(key);
			if(string.IsNullOrWhiteSpace(idPlatform)) idPlatform = null;
			PlayGamesPlatform.Instance.ShowLeaderboardUI(idPlatform);
		}
		
		private void ReportLeaderboard(string key,long value)
		{
			string idPlatform = MnKey.GetValue(key);
			PlayGamesPlatform.Instance.ReportScore(value,idPlatform,(bool success)=>
			{
				if(CanDebug) Debug.Log($"{this.name}:ReportLeaderboard:{success}:{key}:{value}",this);
			});
		}
		
		private void ShowAchievement(string key)
		{
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		
		private void ReportAchievement(string key,float value)
		{
			string idPlatform = MnKey.GetValue(key);
			PlayGamesPlatform.Instance.ReportProgress(idPlatform,value,(bool success)=>
			{
				if(CanDebug) Debug.Log($"{this.name}:ReportAchievement:{success}:{key}:{value}",this);
			});
		}
		#endregion
		
		
		#region Invitation
		private Invitation invitation;
		private UnityAction onInvitationReceive = delegate(){};
		private void OnInvitationReceive(Invitation value, bool shouldAutoAccept)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationReceive:{shouldAutoAccept}:{value}",this);
			invitation = value;
			onInvitationReceive.Invoke();
			if (shouldAutoAccept) OnInvitationAccept();
		}
		
		private UnityAction onInvitationAccept = delegate(){};
		private void OnInvitationAccept()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationAccept",this);
			onInvitationAccept.Invoke();
		}
		
		//PlayGamesPlatform.Instance.TurnBased.DeclineInvitation(mIncomingInvitation.InvitationId);
		private UnityAction onInvitationDecline = delegate(){};
		private void OnInvitationDecline()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationDecline:",this);
			onInvitationDecline.Invoke();
		}
		#endregion
		
		
		#region Match
		private UnityAction onTurnMatchGot = delegate(){};
		private void OnTurnMatchGot(TurnBasedMatch value, bool shouldAutoLaunch)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnTurnMatchGot:{shouldAutoLaunch}:{value}",this);
			onTurnMatchGot.Invoke();
			// MnMatchTurn.ins.shouldAutoLaunch = shouldAutoLaunch;
			// MnMatchTurn.ins.match = value;
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
		[SerializeField]private bool requestEmail;
		[SerializeField]private bool requestServerAuthCode;
		
		[SerializeField]private NodeString displayName;
		[SerializeField]private NodeString idToken;
		[SerializeField]private NodeString authCode;
		#pragma warning restore
	}
}
#endif
#endif
#if xLibv2
#if GooglePlayService
using System;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using xLib.Serialization.Generic;
using xLib.Multi.Player;
using xLib.xNew;

namespace xLib
{
	public class MnMatchTurn : SingletonM<MnMatchTurn>
	{
		public uint variant = 1;
		public uint opponentMin=1;
		public uint opponentMax=7;
		
		public byte turnTarget;
		public uint scoreTarget;
		
		[NonSerialized]public bool shouldAutoLaunch = false;
		[NonSerialized]public TurnBasedMatch match;
		
		[NonSerialized]public bool isMyTurn = false;
		[NonSerialized]public byte indexMy = 0;
		[NonSerialized]public InfoMatch infoMatch = new InfoMatch();
		
		
		
		#region Mono
		protected override void Started()
		{
			TryRegister(true);
		}
		
		protected override void OnDisabled()
		{
			LeaveMatch();
		}
		
		protected override void OnDestroyed()
		{
			TryRegister(false);
		}
		
		private void TryRegister(bool value)
		{
			if (value)
			{
				if(CanDebug) Debug.LogWarning("Init: "+this.name);
				MnPlayService.ins.onTurnMatchGot += OnMatchGot;
				MnPlayService.ins.onInvitationAccept += AcceptInvitation;
			}
			else
			{
				MnPlayService.ins.onTurnMatchGot -= OnMatchGot;
				MnPlayService.ins.onInvitationAccept -= AcceptInvitation;
			}
		}
		#endregion
		
		
		
		
		
		#region CreateMatch
		public void CreateMatchQuick()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": CreateMatchQuick");
			if (!MnSocial.ins.isLogin.Value) return;
			PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(opponentMin, opponentMax, variant, OnMatchStart);
			StAnalytics.LogEvent("Value","CreateMatchQuick",variant.ToString());
		}
		
		public void CreateMatchInvite()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": CreateMatchInvite");
			if (!MnSocial.ins.isLogin.Value) return;
			PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(opponentMin, opponentMax, variant, OnMatchStart);
			StAnalytics.LogEvent("Value","CreateMatchInvite",variant.ToString());
		}
		#endregion
		
		
		
		
		
		#region AcceptInvitation
		public void AcceptFromInbox()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": AcceptFromInbox");
			if (!MnSocial.ins.isLogin.Value) return;
			PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnInvitationAccept);
		}
		
		private void AcceptInvitation()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": AcceptInvitation");
			PlayGamesPlatform.Instance.TurnBased.AcceptInvitation(MnPlayService.ins.invitation.InvitationId, OnInvitationAccept);
		}
		
		public UnityAction onInvitationAccept = delegate(){};
		private void OnInvitationAccept(bool success, TurnBasedMatch acceptedMatch)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+": OnInvitationAccept:{0}",success);
			if (!success) return;
			onInvitationAccept.Invoke();
			OnMatchStart(success, acceptedMatch);
		}
		#endregion
		
		
		
		
		
		#region OnMatchGot
		private void OnMatchGot()
		{
			if (!shouldAutoLaunch)
			{
				Debug.LogWarning("Your turn is up!");
				return;
			}
			OnMatchStart(true, match);
		}
		#endregion
		
		
		
		
		
		
		#region OnMatchStart
		public UnityAction onMatchStart = delegate(){};
		private void OnMatchStart(bool success, TurnBasedMatch startedMatch)
		{
			if(CanDebug) Debug.Log("OnMatchStart: " + success);
			isMyTurn = false;
			
			if (!success) return;
			match = startedMatch;
			SetupInfoMatch();
			onMatchStart.Invoke();
			
			switch (match.Status)
			{
				case TurnBasedMatch.MatchStatus.Active:
				case TurnBasedMatch.MatchStatus.AutoMatching:
					OnTurnStart();
					break;
				case TurnBasedMatch.MatchStatus.Complete:
					AcknowledgeFinished();
					break;
				case TurnBasedMatch.MatchStatus.Cancelled:
				case TurnBasedMatch.MatchStatus.Deleted:
				case TurnBasedMatch.MatchStatus.Expired:
				case TurnBasedMatch.MatchStatus.Unknown:
					OnMatchFinish(true);
					break;
			}
		}
		#endregion
		
		
		
		
		
		
		
		#region StartTurn
		public UnityAction onTurnStart = delegate(){};
		public UnityAction onTurnYour = delegate(){};
		public UnityAction onTurnOther = delegate(){};
		private void OnTurnStart()
		{
			if(CanDebug) Debug.Log("onTurnStart");
			isMyTurn = (match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
			if(isMyTurn) infoMatch.curTurn++;
			onTurnStart.Invoke();
			
			if (isMyTurn) 
			{
				if(CanDebug) Debug.Log("onTurnYour");
				onTurnYour.Invoke();
			}
			else
			{
				if(CanDebug) Debug.Log("onTurnOther");
				onTurnOther.Invoke();
				ClearMatch();
			}
		}
		#endregion
		
		
		
		
		
		
		#region EndTurn
		private string NextPlayer()
		{
			string nextPlayerId = null;
			int indexNextPlayer=0;
			if (indexMy + 1 < infoMatch.onlineProfile.Length) indexNextPlayer = indexMy + 1;
			if (indexNextPlayer < match.Participants.Count) nextPlayerId = match.Participants[indexNextPlayer].ParticipantId;
			
			if(CanDebug) Debug.Log("indexNextPlayer: " + indexNextPlayer);
			if(CanDebug) Debug.Log("NextPlayerId: " + nextPlayerId);
			return nextPlayerId;
		}
		
		public void EndTurn()
		{
			TryEndMatch();
			
			if(CanDebug) Debug.Log("EndTurn");
			if (match == null) return;
			if (!isMyTurn) return;
			infoMatch.RefreshLists();
			PlayGamesPlatform.Instance.TurnBased.TakeTurn(match, SerializerByteArray.Serialize(infoMatch), NextPlayer(), OnTurnEnd);
		}
		
		public UnityAction onTurnEnd = delegate(){};
		private void OnTurnEnd(bool success)
		{
			if(CanDebug) Debug.Log("OnTurnEnd: " + success);
			if (!success) return;
			onTurnEnd.Invoke();
			ClearMatch();
		}
		#endregion
		
		
		
		
		
		#region TryEndMatch
		private void TryEndMatch()
		{
			if(CanDebug) Debug.Log("TryEndMatch");
			if (match == null) return;
			if (TryEndMaxTurn()) return;
			if (TryEndMaxScore()) return;
			
			//EndTurn();
		}
		
		private bool TryEndMaxTurn()
		{
			if (turnTarget == 0) return false;
			if (infoMatch.curTurn < (turnTarget*infoMatch.onlineProfile.Length)) return false;
			
			if(CanDebug) Debug.Log("EndMaxTurn");
			FinishMatch();
			return true;
		}
		
		private bool TryEndMaxScore()
		{
			if (scoreTarget == 0) return false;
			if (infoMatch.onlineProfile[indexMy].score < scoreTarget) return false;
			
			if(CanDebug) Debug.Log("EndMaxScore");
			FinishMatch();
			return true;
		}
		#endregion
		
		
		
		
		
		
		#region EndMatch
		private void FinishMatch()
		{
			if (!isMyTurn) return;
			if(CanDebug) Debug.Log("FinishMatch");
			infoMatch.RefreshLists();
			
			MatchOutcome outCome = new MatchOutcome();
			MatchOutcome.ParticipantResult result = MatchOutcome.ParticipantResult.Loss;
			
			for (int i = 0; i < match.Participants.Count; i++)
			{
				uint rank = infoMatch.onlineProfile[i].rank;
				
				result = MatchOutcome.ParticipantResult.Loss;
				if(rank==0) result = MatchOutcome.ParticipantResult.Win;
				
				outCome.SetParticipantResult(match.Participants[i].ParticipantId, result, rank+1);
			}
			
			PlayGamesPlatform.Instance.TurnBased.Finish(match, SerializerByteArray.Serialize(infoMatch), outCome, OnMatchFinish);
		}
		
		private void AcknowledgeFinished()
		{
			if(CanDebug) Debug.Log("AcknowledgeFinished");
			PlayGamesPlatform.Instance.TurnBased.AcknowledgeFinished(match,OnMatchFinish);
		}
		
		public UnityAction onMatchFinish = delegate(){};
		private void OnMatchFinish(bool success)
		{
			if(CanDebug) Debug.Log("OnMatchFinish: " + success);
			if (!success) return;
			onMatchFinish.Invoke();
			ClearMatch();
		}
		#endregion
		
		
		
		
		
		
		#region LeaveMatch
		public void LeaveMatch()
		{
			if (match == null) return;
			if(CanDebug) Debug.Log("LeaveMatch");
			
			if (match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn)
			{
				PlayGamesPlatform.Instance.TurnBased.LeaveDuringTurn(match, NextPlayer(), OnMatchLeave);
			}
			else
			{
				PlayGamesPlatform.Instance.TurnBased.Leave(match, OnMatchLeave);
			}
		}
		
		public UnityAction onMatchLeave = delegate(){};
		private void OnMatchLeave(bool success)
		{
			if(CanDebug) Debug.Log("OnMatchLeave: " + success);
			if (!success) return;
			onMatchLeave.Invoke();
			match = null;
		}
		#endregion
		
		
		
		
		
		#region ClearMatch
		private void ClearMatch()
		{
			if(CanDebug) Debug.Log("ClearMatch");
			if (match == null) return;
			match = null;
			infoMatch = new InfoMatch();
			OnMatchClear();
		}
		
		public UnityAction onMatchClear = delegate(){};
		private void OnMatchClear()
		{
			if(CanDebug) Debug.Log("OnMatchClear");
			onMatchClear.Invoke();
		}
		#endregion
		
		
		
		
		
		
		
		
		//Fix Match Starting!
		//OptimizeHere
		private void SetupInfoMatch()
		{
			if(CanDebug) Debug.Log("SetupInfoMatch: " + (match != null));
			if (match == null) return;
			
			int expectedPlayer;
			expectedPlayer = match.Participants.Count;
			expectedPlayer += (int)match.AvailableAutomatchSlots;
			if(CanDebug) Debug.Log("expectedPlayer: " + expectedPlayer.ToString());
			
			#region SetOnlineProfiles
			infoMatch = new InfoMatch();
			if (match.Data != null && match.Data.Length > 0)
			{
				if(CanDebug) Debug.Log("DataLength: " + match.Data.Length);
				SerializerByteArray.TryDeserialize(ref infoMatch, match.Data);
			}
			
			if (infoMatch.onlineProfile == null)
			{
				if(CanDebug) Debug.Log("new onlineProfile");
				infoMatch.onlineProfile = new OnlineProfile[expectedPlayer];
				infoMatch.onlineProfile.Initialize();
			}
			else if (infoMatch.onlineProfile.Length < expectedPlayer)
			{
				if(CanDebug) Debug.Log("increase onlineProfile");
				ExtArray.IncreaseLenght(ref infoMatch.onlineProfile, expectedPlayer-1);
			}
			
			if(CanDebug) Debug.Log("Set indexes");
			for (byte i = 0; i < infoMatch.onlineProfile.Length; i++)
			{
				infoMatch.onlineProfile[i].index = i;
			}
			#endregion
			
			#region FindMyIndex
			if(CanDebug) Debug.Log("FindMyIndex");
			for (byte i = 0; i < match.Participants.Count; i++)
			{
				if (match.Participants[i].ParticipantId == match.SelfParticipantId)
				{
					indexMy = i;
					infoMatch.onlineProfile[i].isMine = true;
					break;
				}
			}
			#endregion
			infoMatch.RefreshLists();
			
			DebugPlayers();
		}
		
		
		
		
		
		private void DebugPlayers()
		{
			if(!CanDebug) return;
			
			Debug.Log("indexMy: " + indexMy);
			for (int i = 0; i < match.Participants.Count; i++)
			{
				Debug.Log(match.Participants[i].DisplayName);
				Debug.Log(match.Participants[i].ParticipantId);
				Debug.Log("index: " + infoMatch.onlineProfile[i].index);
				Debug.Log("Score: " + infoMatch.onlineProfile[i].score);
			}
			Debug.Log("AvailableSlots: " + match.AvailableAutomatchSlots);
		}
		
		
		
		
		
	}
}
#endif
#endif
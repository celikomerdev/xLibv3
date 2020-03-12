#if xLibv3
#if GooglePlayService
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnPlayServiceInvitation : SingletonM<MnPlayServiceInvitation>
	{
		private Invitation invitation = null;
		
		[SerializeField]private EventUnity onInvitationReceive = new EventUnity();
		private void OnInvitationReceive(Invitation value, bool shouldAutoAccept)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationReceive:{shouldAutoAccept}:{value}",this);
			invitation = value;
			onInvitationReceive.Invoke();
			if (shouldAutoAccept) OnInvitationAccept();
		}
		internal static void InvitationDelegate(Invitation value, bool shouldAutoAccept)
		{
			ins?.OnInvitationReceive(value,shouldAutoAccept);
		}
		
		[SerializeField]private EventBool onInvitationAccept = new EventBool();
		private void OnInvitationAccept()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationAccept",this);
			onInvitationAccept.Invoke(true);
		}
		
		//PlayGamesPlatform.Instance.TurnBased.DeclineInvitation(mIncomingInvitation.InvitationId);
		private void OnInvitationDecline()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInvitationDecline:",this);
			onInvitationAccept.Invoke(false);
		}
	}
}
#endif
#endif
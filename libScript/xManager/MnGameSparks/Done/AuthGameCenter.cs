#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class AuthGameCenter : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private bool doNotCreateNewPlayer;
		[SerializeField]private bool doNotLinkToCurrentPlayer;
		[SerializeField]private bool errorOnSwitch;
		[SerializeField]private bool switchIfPossible;
		[SerializeField]private bool syncDisplayName;
		
		[SerializeField]private NodeString displayName;
		[SerializeField]private NodeString externalPlayerId;
		[SerializeField]private NodeString publicKeyUrl;
		[SerializeField]private NodeString salt;
		[SerializeField]private NodeString signature;
		[SerializeField]private NodeInt timestamp; //TODO long
		[SerializeField]private EventBool eventBool;
		
		#region Call
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			GameCenterConnectRequest request = new GameCenterConnectRequest();
			request.SetDurable(isDurable);
			
			request.SetDoNotCreateNewPlayer(doNotCreateNewPlayer);
			request.SetDoNotLinkToCurrentPlayer(doNotLinkToCurrentPlayer);
			request.SetErrorOnSwitch(errorOnSwitch);
			request.SetSwitchIfPossible(switchIfPossible);
			request.SetSyncDisplayName(syncDisplayName);
			
			request.SetDisplayName(displayName.Value);
			request.SetExternalPlayerId(externalPlayerId.Value);
			request.SetPublicKeyUrl(publicKeyUrl.Value);
			request.SetSalt(salt.Value);
			request.SetSignature(signature.Value);
			request.SetTimestamp(timestamp.Value);
			
			request.Send(Callback);
		}
		
		private void Callback(AuthenticationResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			eventBool.Invoke(!response.HasErrors);
		}
		#endregion
	}
}
#endif
#endif
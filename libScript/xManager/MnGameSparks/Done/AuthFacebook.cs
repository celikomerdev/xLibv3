#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class AuthFacebook : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private bool doNotCreateNewPlayer;
		[SerializeField]private bool doNotLinkToCurrentPlayer;
		[SerializeField]private bool errorOnSwitch;
		[SerializeField]private bool switchIfPossible;
		[SerializeField]private bool syncDisplayName;
		
		[SerializeField]private NodeString token;
		[SerializeField]private EventBool eventBool;
		
		#region Call
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			FacebookConnectRequest request = new FacebookConnectRequest();
			request.SetDurable(isDurable);
			
			request.SetDoNotCreateNewPlayer(doNotCreateNewPlayer);
			request.SetDoNotLinkToCurrentPlayer(doNotLinkToCurrentPlayer);
			request.SetErrorOnSwitch(errorOnSwitch);
			request.SetSwitchIfPossible(switchIfPossible);
			request.SetSyncDisplayName(syncDisplayName);
			
			request.SetAccessToken(token.Value);
			//request.SetCode("code");
			
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
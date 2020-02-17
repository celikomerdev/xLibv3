#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class AuthUser : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private NodeString userName;
		[SerializeField]private NodeString userPass;
		[SerializeField]private EventBool eventBool;
		
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			AuthenticationRequest request = new AuthenticationRequest();
			request.SetDurable(isDurable);
			
			if(string.IsNullOrEmpty(userName.Value)) return;
			request.SetUserName(userName.Value);
			
			if(string.IsNullOrEmpty(userPass.Value)) return;
			request.SetPassword(userPass.Value);
			
			request.Send(Callback);
		}
		
		private void Callback(AuthenticationResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			eventBool.Invoke(!response.HasErrors);
		}
	}
}
#endif
#endif
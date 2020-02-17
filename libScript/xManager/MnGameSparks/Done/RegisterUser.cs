#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class RegisterUser : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private NodeString userName;
		[SerializeField]private NodeString userPass;
		[SerializeField]private EventBool eventBool;
		
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			RegistrationRequest request = new RegistrationRequest();
			request.SetDurable(isDurable);
			
			if(string.IsNullOrEmpty(userName.Value)) return;
			request.SetUserName(userName.Value);
			request.SetDisplayName(userName.Value);
			
			if(string.IsNullOrEmpty(userPass.Value)) return;
			request.SetPassword(userPass.Value);
			
			request.Send(Callback);
		}
		
		private void Callback(RegistrationResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			UseResponse(response);
			eventBool.Invoke(!response.HasErrors);
		}
		
		private void UseResponse(RegistrationResponse response)
		{
			if(response.HasErrors)
			{
				if(response.Errors.GetString("USERNAME") == "TAKEN") StPopupBar.MessageLocalized("username not avaible");
				return;
			}
			//MnGameSpark.ins.displayName.Value = response.DisplayName;
		}
	}
}
#endif
#endif
#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class AuthDevice : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private EventBool eventBool;
		
		#region Call
		private bool inCall;
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			if(inCall) return;
			inCall = true;
			
			DeviceAuthenticationRequest request = new DeviceAuthenticationRequest();
			request.SetDurable(isDurable);
			
			//request.SetDisplayName(MnGameSpark.ins.displayName.Value);
			
			request.Send(Callback);
		}
		
		private void Callback(AuthenticationResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			inCall = false;
			eventBool.Invoke(!response.HasErrors);
		}
		#endregion
	}
}
#endif
#endif
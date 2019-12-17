#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class ChangeUserPass : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private NodeString userPassOld;
		[SerializeField]private NodeString userPassNew;
		[SerializeField]private EventBool eventBool;
		
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			ChangeUserDetailsRequest request = new ChangeUserDetailsRequest();
			request.SetDurable(isDurable);
			
			if(string.IsNullOrEmpty(userPassOld.Value)) return;
			request.SetOldPassword(userPassOld.Value);
			
			if(string.IsNullOrEmpty(userPassNew.Value)) return;
			request.SetNewPassword(userPassNew.Value);
			
			request.Send(Callback);
		}
		
		private void Callback(ChangeUserDetailsResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			eventBool.Invoke(!response.HasErrors);
		}
	}
}
#endif
#endif
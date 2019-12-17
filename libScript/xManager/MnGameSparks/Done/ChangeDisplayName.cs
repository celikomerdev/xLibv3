#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class ChangeDisplayName : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private NodeString displayName;
		[SerializeField]private EventBool eventBool;
		
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			ChangeUserDetailsRequest request = new ChangeUserDetailsRequest();
			request.SetDurable(isDurable);
			
			if(string.IsNullOrEmpty(displayName.Value)) return;
			request.SetDisplayName(displayName.Value);
			
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
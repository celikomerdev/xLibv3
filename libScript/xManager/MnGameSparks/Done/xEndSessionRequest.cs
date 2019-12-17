#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class xEndSessionRequest : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		[SerializeField]private NodeBool isLogin;
		[SerializeField]private EventBool eventBool;
		
		#region Call
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			if(!isLogin.Value) return;
			
			EndSessionRequest request = new EndSessionRequest();
			request.SetDurable(isDurable);
			
			request.Send(Callback);
		}
		
		private void Callback(EndSessionResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			eventBool.Invoke(!response.HasErrors);
		}
		#endregion
	}
}
#endif
#endif
#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;

namespace xLib.xGameSparks
{
	public class ServiceListLeaderboards : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		
		#region Call
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			ListLeaderboardsRequest request = new ListLeaderboardsRequest();
			request.SetDurable(isDurable);
			
			request.Send(Callback);
		}
		
		private void Callback(ListLeaderboardsResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
		}
		#endregion
	}
}
#endif
#endif
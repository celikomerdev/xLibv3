#if xLibv2
#if GameSparks
using System.Linq;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;

namespace xLib.xGameSparks
{
	public class LeaderboardAroundTop : BaseRegisterM
	{
		[SerializeField]private bool isDurable;
		
		[Header("Request")]
		[SerializeField]private int count = 10;
		[SerializeField]private bool social;
		[SerializeField]private string key = "Leaderboard";
		[SerializeField]private NodeString[] keyValue;
		
		#region Mono
		protected override bool TryRegister(bool value)
		{
			if(value && baseRegister.onRegister)
			{
				Call();
			}
			return value;
		}
		#endregion
		
		#region Call
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			viewCount.Value = 0;
			
			ApplyViewId();
			
			LeaderboardDataRequest request = new LeaderboardDataRequest();
			request.SetDurable(isDurable);
			
			request.SetEntryCount(count);
			request.SetSocial(social);
			
			string tempKey = key;
			for (int i = 0; i < keyValue.Length; i++)
			{
				tempKey += "."+keyValue[i].Key;
				tempKey += "."+keyValue[i].Value;
			}
			request.SetLeaderboardShortCode(tempKey);
			request.Send(Callback);
			
			ApplyLastId();
		}
		
		[Header("Result")]
		[SerializeField]private MonoInt viewCount;
		[SerializeField]private MonoInt viewIndex;
		[SerializeField]private NodeString[] nodeString;
		[SerializeField]private NodeInt[] nodeInt;
		private void Callback(LeaderboardDataResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			
			if(response.HasErrors)
			{
				viewCount.Value = 0;
				//StPopupBar.MessageLocalized("loading failed");
				return;
			}
			
			viewCount.Value = response.Data.Count();
			int tempIndex = 0;
			ApplyViewId();
			
			foreach (var entry in response.Data)
			{
				MnPlayer.CurrentId = entry.UserId;
				for (int i = 0; i < nodeString.Length; i++)
				{
					if(entry.JSONData.ContainsKey(nodeString[i].Key))
					{
						nodeString[i].Value = entry.JSONData[nodeString[i].Key].ToString();
					}
				}
				for (int i = 0; i < nodeInt.Length; i++)
				{
					if(entry.JSONData.ContainsKey(nodeInt[i].Key))
					{
						nodeInt[i].Value = (int)entry.JSONData[nodeInt[i].Key];
					}
				}
				
				viewIndex.Value = tempIndex;
				tempIndex++;
			}
			
			ApplyLastId();
		}
		#endregion
	}
}
#endif
#endif
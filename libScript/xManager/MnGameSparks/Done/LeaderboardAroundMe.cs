#if xLibv2
#if GameSparks
using System;
using System.Linq;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class LeaderboardAroundMe : BaseRegisterM
	{
		[SerializeField]private bool isDurable;
		
		[Header("Request")]
		[SerializeField]private int count = 10;
		[SerializeField]private int top = 10;
		[SerializeField]private int bottom = 10;
		[SerializeField]private bool social;
		[SerializeField]private string key = "Leaderboard";
		[SerializeField]private NodeString[] keyValue;
		
		[SerializeField]private EventBool eventBool;
		
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
			
			AroundMeLeaderboardRequest request = new AroundMeLeaderboardRequest();
			request.SetDurable(isDurable);
			
			request.SetIncludeFirst(top);
			request.SetIncludeLast(bottom);
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
		private void Callback(AroundMeLeaderboardResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			UseResponse(response);
			eventBool.Invoke(!response.HasErrors);
		}
		
		private void UseResponse(AroundMeLeaderboardResponse response)
		{
			if(response.HasErrors)
			{
				viewCount.Value = 0;
				return;
			}
			
			int totalCount = 0;
			totalCount += response.First.Count();
			totalCount += response.Data.Count();
			totalCount += response.Last.Count();
			viewCount.Value = totalCount;
			
			ApplyViewId();
			
			tempIndex = 0;
			CreateViews(response.First);
			CreateViews(response.Data);
			CreateViews(response.Last);
			
			ApplyLastId();
		}
		
		private int tempIndex = 0;
		private void CreateViews(GSEnumerable<AroundMeLeaderboardResponse._LeaderboardData> entries)
		{
			foreach (var entry in entries)
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
						nodeInt[i].Value = int.Parse(entry.JSONData[nodeInt[i].Key].ToString());
					}
				}
				
				viewIndex.Value = tempIndex;
				tempIndex++;
			}
		}
		#endregion
	}
}
#endif
#endif
#if xLibv2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using xLib.Serialization.Generic;

namespace xLib.xNew
{
	[Serializable]
	public class InfoMatch : ISerializable
	{
		public byte curTurn;
		public OnlineProfile[] onlineProfile = new OnlineProfile[0];
		public List<OnlineProfile> indexScore;
		
		public InfoMatch()
		{
			
		}
		
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("curTurn", curTurn);
			info.AddValue("onlineProfile", onlineProfile);
		}
		
		public InfoMatch(SerializationInfo info, StreamingContext context)
		{
			info.TryGetByte("curTurn", ref curTurn);
			info.TryGetObject("onlineProfile", ref onlineProfile);
		}
		
		#region Sorting
		public void RefreshLists()
		{
			xDebug.LogTempFormat("InfoMatch:RefreshLists");
			indexScore = onlineProfile.ToList();
			SortLists();
		}
		
		public void SortLists()
		{
			SortByScore();
		}
		
		public void SortByScore()
		{
			xDebug.LogTempFormat("InfoMatch:SortByScore");
			indexScore = indexScore.OrderByDescending(x => x.score).ToList();
			for (byte i = 0; i < indexScore.Count; i++)
			{
				indexScore[i].sortScore = i;
			}
		}
		#endregion
	}
}
#endif
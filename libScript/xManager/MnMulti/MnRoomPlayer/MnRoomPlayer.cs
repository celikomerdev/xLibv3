#if xLibv2
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace xLib.xNew
{
	public class MnRoomPlayer : SingletonM<MnRoomPlayer>
	{
		[SerializeField]private NodeBool canUpdatePlayers;
		[SerializeField]private NodeBool isPlayerJoined;
		[SerializeField]private NodeVoid onUpdatePlayers;
		
		[SerializeField]internal NodeInt playerCount;
		[SerializeField]internal NodeInt playerIndexId;
		
		
		private Dictionary<string,OnlinePlayer> dictPlayer = new Dictionary<string,OnlinePlayer>();
		internal List<OnlinePlayer> listPlayer = new List<OnlinePlayer>();
		
		public void PlayerJoined(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":PlayerJoined:{0}",id);
			
			OnlinePlayer onlinePlayer = new OnlinePlayer();
			onlinePlayer.id = id;
			dictPlayer.Add(id,onlinePlayer);
			
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = id;
			
			isPlayerJoined.Value = true;
			
			MnPlayer.CurrentId = tempId;
			
			UpdatePlayers();
		}
		
		public void PlayerLeft(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":PlayerLeft:{0}",id);
			
			dictPlayer.Remove(id);
			
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = id;
			
			isPlayerJoined.Value = false;
			
			MnPlayer.CurrentId = tempId;
			
			UpdatePlayers();
		}
		
		public void UpdatePlayers()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":UpdatePlayers");
			if(!canUpdatePlayers.Value) return;
			
			listPlayer.Clear();
			
			int index = 0;
			string tempId = MnPlayer.CurrentId;
			foreach (KeyValuePair<string,OnlinePlayer> pair in dictPlayer.OrderBy(pair => pair.Key))
			{
				OnlinePlayer onlinePlayer = pair.Value;
				MnPlayer.CurrentId = onlinePlayer.id;
				
				onlinePlayer.indexId = index;
				playerIndexId.Value = index;
				index++;
				
				listPlayer.Add(onlinePlayer);
			}
			MnPlayer.CurrentId = tempId;
			
			playerCount.Value = listPlayer.Count;
			onUpdatePlayers.Call();
		}
		
		public void ClearPlayers()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ClearPlayers");
			if(!canUpdatePlayers.Value) return;
			
			SetOfflinePlayers();
			
			dictPlayer.Clear();
			listPlayer.Clear();
			UpdatePlayers();
		}
		
		private void SetOfflinePlayers()
		{
			if(!canUpdatePlayers.Value) return;
			string tempId = MnPlayer.CurrentId;
			
			for (int i = 0; i < listPlayer.Count; i++)
			{
				MnPlayer.CurrentId = listPlayer[i].id;
				isPlayerJoined.Value = false;
			}
			
			MnPlayer.CurrentId = tempId;
		}
	}
}
#endif
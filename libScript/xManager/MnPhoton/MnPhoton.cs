#if xLibv2
#if Photon
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using xLib.xNew;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnPhoton : SingletonM<MnPhoton>
	{
		public PhotonView photonView;
		
		
		#region Mono
		protected override void OnDestroyed()
		{
			Connect(false);
		}
		#endregion
		
		
		#region ConnectDisconnect
		public void Connect(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Connect:{0}",value);
			
			if(MnMulti.ins.isConnect.Value == value) return;
			if(MnMulti.ins.isBusy.Value) return;
			MnMulti.ins.isBusy.Value = true;
			
			if(value) Connect();
			else Disconnect();
		}
		
		private void Connect()
		{
			PhotonNetwork.OfflineMode = MnMulti.ins.isOfflineMode.Value;
			if(MnMulti.ins.isOfflineMode.Value) return;
			PhotonNetwork.ConnectUsingSettings();
		}
		
		public void SetCustomProperties()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":SetCustomProperties");
			Hashtable hastable = new Hashtable();
			hastable.Add(MnPlayerData.ins.nodeGroup.Key,MnPlayerData.ins.nodeGroup.SerializedObjectRaw.ToString());
			PhotonNetwork.LocalPlayer.SetCustomProperties(hastable);
		}
		
		public void GetCustomProperties()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":GetCustomProperties");
			object outObject;
			Player player = PhotonNetwork.CurrentRoom.GetPlayer(int.Parse(MnPlayer.CurrentId));
			if(player.CustomProperties.TryGetValue(MnPlayerData.ins.nodeGroup.Key,out outObject))
			{
				MnPlayerData.ins.nodeGroup.SerializedObjectRaw = outObject;
			}
		}
		
		private void Disconnect()
		{
			PhotonNetwork.Disconnect();
			PhotonNetwork.SendAllOutgoingCommands();
		}
		#endregion
		
		
		#region JoinCreate
		public void JoinOrCreateRoom(bool value = false)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":JoinOrCreateRoom:{0}",value);
			
			if(!PhotonNetwork.IsConnectedAndReady) return;
			if(MnMulti.ins.inRoom.Value) return;
			if(MnMulti.ins.isBusy.Value) return;
			MnMulti.ins.isBusy.Value = true;
			
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = (byte)MnNew.ins.maxPlayers.Value;
			roomOptions.CleanupCacheOnLeave = true;
			
			roomOptions.CustomRoomProperties = new Hashtable();
			//TODO add properties generic
			roomOptions.CustomRoomPropertiesForLobby = new string[]{MnNew.ins.roomMap.Key,MnNew.ins.roomTier.Key};
			roomOptions.CustomRoomProperties.Add(MnNew.ins.roomMap.Key,MnNew.ins.roomMap.Value);
			roomOptions.CustomRoomProperties.Add(MnNew.ins.roomTier.Key,MnNew.ins.roomTier.Value);
			
			if(MnNew.ins.maxPlayers.Value==1) value = true;
			if(value)
			{
				PhotonNetwork.CreateRoom(null,roomOptions,TypedLobby.Default);
			}
			else
			{
				PhotonNetwork.JoinRandomRoom(roomOptions.CustomRoomProperties,0);
			}
		}
		#endregion
		
		
		#region Leave
		public void LeaveRoom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LeaveRoom");
			
			if(!MnMulti.ins.inRoom.Value) return;
			if(MnMulti.ins.isBusy.Value) return;
			MnMulti.ins.isBusy.Value = true;
			
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.SendAllOutgoingCommands();
		}
		#endregion
		
		
		#region Setter
		#region GameVersion
		public string GameVersion
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":GameVersion:{0}",value);
				PhotonNetwork.GameVersion = value;
			}
		}
		#endregion
		
		
		#region IsMessageQueueRunning
		public bool IsMessageQueueRunning
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":IsMessageQueueRunning:{0}",value);
				PhotonNetwork.IsMessageQueueRunning = value;
			}
		}
		#endregion
		
		
		#region SendRate
		public int SendRate
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":SendRate:{0}",value);
				MnNew.sendInterval = 1f/value;
				PhotonNetwork.SendRate = value;
				PhotonNetwork.SerializationRate = value;
			}
		}
		#endregion
		
		
		#region IsOpen
		public bool IsOpen
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":IsOpen:{0}",value);
				if(MnMulti.ins.isOfflineMode.Value) return;
				if(!MnMulti.ins.inRoom.Value) return;
				
				PhotonNetwork.CurrentRoom.IsOpen = value;
				PhotonNetwork.CurrentRoom.IsVisible = value;
			}
		}
		#endregion
		#endregion
		
		
		#region RPC
		public void RPC(xRpcTarget target,params object[] parameters)
		{
			photonView.RPC("AllRpc",(RpcTarget)target,parameters);
		}
		
		[PunRPC]private void AllRpc(string key,string data,PhotonMessageInfo info)
		{
			MnRPC.ins.AllRpc(info.Sender.ActorNumber.ToString(),key,data);
		}
		#endregion
	}
}
#else
namespace xLib
{
	public class MnPhoton : SingletonM<MnPhoton>
	{
		public void Connect(bool value){}
		public void SetCustomProperties(){}
		public void GetCustomProperties(){}
		public void JoinOrCreateRoom(bool value = false){}
		public void LeaveRoom(){}
		
		public string GameVersion{set{}}
		public bool IsMessageQueueRunning{set{}}
		public int SendRate{set{}}
		public bool IsOpen{set{}}
		
		public void RPC(xRpcTarget target,params object[] parameters){}
	}
}
#endif
#endif
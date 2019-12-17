#if xLibv2
#if Photon
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace xLib.xPhoton
{
	public class ListenerPhoton : BaseWorkM, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks
	{
		
		#region Mono
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
		
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		#endregion
		
		
		
		#region Callback
		
		#region IConnectionCallbacks
		void IConnectionCallbacks.OnConnected()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnConnected");
		}
		
		void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegionListReceived");
		}
		
		void IConnectionCallbacks.OnConnectedToMaster()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnConnectedToMaster");
			MnMulti.ins.OnConnect();
		}
		
		void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisconnected");
			MnMulti.ins.OnDisconnect();
		}
		
		void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCustomAuthenticationFailed");
		}
		
		void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCustomAuthenticationResponse");
		}
		#endregion
		
		
		
		#region IMatchmakingCallbacks
		void IMatchmakingCallbacks.OnCreatedRoom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCreatedRoom");
			MnMulti.ins.OnRoomCreateSuccess();
		}
		
		void IMatchmakingCallbacks.OnCreateRoomFailed(short returnCode, string message)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCreateRoomFailed");
			MnMulti.ins.OnRoomCreateFail();
		}
		
		void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnFriendListUpdate");
		}
		
		void IMatchmakingCallbacks.OnJoinedRoom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnJoinedRoom");
			string tempId = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
			
			MnMulti.ins.OnRoomJoinSuccess(tempId);
			MnMulti.ins.OnRoomPlayerEnter(tempId);
			MnMulti.ins.OnMasterClientSwitched(PhotonNetwork.MasterClient.ActorNumber.ToString());
		}
		
		void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnJoinRandomFailed");
			MnMulti.ins.OnRoomJoinFailRandom();
		}
		
		void IMatchmakingCallbacks.OnJoinRoomFailed(short returnCode, string message)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnJoinRoomFailed");
			MnMulti.ins.OnRoomJoinFail();
		}
		
		void IMatchmakingCallbacks.OnLeftRoom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLeftRoom");
			MnMulti.ins.OnRoomPlayerExit(MnPlayer.MyId);
			MnMulti.ins.OnRoomLeft();
		}
		#endregion
		
		
		
		#region IInRoomCallbacks
		void IInRoomCallbacks.OnMasterClientSwitched(Player player)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnMasterClientSwitched");
			MnMulti.ins.OnMasterClientSwitched(player.ActorNumber.ToString());
		}
		
		void IInRoomCallbacks.OnPlayerEnteredRoom(Player player)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPlayerEnteredRoom");
			//MnMulti.ins.OnRoomPlayerEnter(player.ActorNumber);
			//Not works for self
			//TODO check Not works for previous players
		}
		
		void IInRoomCallbacks.OnPlayerLeftRoom(Player player)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPlayerLeftRoom");
			MnMulti.ins.OnRoomPlayerExit(player.ActorNumber.ToString());
			//Not works for self
			//TODO check Not works for previous players
			//TODO only call from master : OnRoomPlayerExit
		}
		
		void IInRoomCallbacks.OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPlayerPropertiesUpdate");
		}
		
		void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomPropertiesUpdate");
		}
		#endregion
		
		
		
		#region ILobbyCallbacks
		void ILobbyCallbacks.OnJoinedLobby()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnJoinedLobby");
		}
		
		void ILobbyCallbacks.OnLeftLobby()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLeftLobby");
		}
		
		void ILobbyCallbacks.OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLobbyStatisticsUpdate");
		}
		
		void ILobbyCallbacks.OnRoomListUpdate(List<RoomInfo> roomList)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomListUpdate");
		}
		#endregion
		
		#endregion
		
		
		
	}
}
#endif
#endif
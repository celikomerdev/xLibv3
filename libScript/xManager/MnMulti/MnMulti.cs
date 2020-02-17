#if xLibv2
using UnityEngine;

namespace xLib
{
	public class MnMulti : SingletonM<MnMulti>
	{
		#region CallbackGeneral
		[Header("CallbackGeneral")]
		[SerializeField]internal NodeBool isOfflineMode;
		[SerializeField]internal NodeBool isBusy;
		#endregion
		
		
		#region CallbackConnection
		[Header("CallbackConnection")]
		[SerializeField]internal NodeBool isConnect;
		internal void OnConnect()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnConnect");
			isBusy.Value = false;
			isConnect.Value = true;
		}
		
		internal void OnDisconnect()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisconnect");
			isBusy.Value = false;
			isConnect.Value = false;
		}
		#endregion
		
		
		#region CallbackRoom
		[Header("CallbackRoom")]
		[SerializeField]public NodeBool inRoom;
		[SerializeField]private NodeVoid onRoomCreateFail;
		internal void OnRoomCreateFail()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomCreateFail");
			isBusy.Value = false;
			onRoomCreateFail.Call();
		}
		
		[SerializeField]private NodeVoid onRoomCreateSuccess;
		internal void OnRoomCreateSuccess()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomCreateSuccess");
			//isBusy.Value = false;
			//inRoom.Value = true;
			onRoomCreateSuccess.Call();
		}
		
		[SerializeField]private NodeVoid onRoomJoinFail;
		internal void OnRoomJoinFail()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomJoinFail");
			isBusy.Value = false;
			onRoomJoinFail.Call();
		}
		
		[SerializeField]private NodeVoid onRoomJoinFailRandom;
		internal void OnRoomJoinFailRandom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomJoinFailRandom");
			isBusy.Value = false;
			onRoomJoinFailRandom.Call();
		}
		
		[SerializeField]private NodeVoid onRoomJoinSuccess;
		internal void OnRoomJoinSuccess(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomJoinSuccess");
			
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = id;
			
			MnPlayer.MyId = id;
			isBusy.Value = false;
			NodeSetting.inRoom = true;
			inRoom.Value = true;
			onRoomJoinSuccess.Call();
			
			MnPlayer.CurrentId = tempId;
		}
		
		[SerializeField]private NodeVoid onRoomLeft;
		internal void OnRoomLeft()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomLeft");
			isBusy.Value = false;
			NodeSetting.inRoom = false;
			inRoom.Value = false;
			onRoomLeft.Call();
		}
		#endregion
		
		
		#region CallbackPlayer
		[Header("CallbackPlayer")]
		[SerializeField]private NodeString onRoomPlayerEnter;
		internal void OnRoomPlayerEnter(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomPlayerEnter:{0}",id);
			
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = id;
			
			onRoomPlayerEnter.Value = id;
			
			MnPlayer.CurrentId = tempId;
		}
		
		[SerializeField]private NodeString onRoomPlayerExit;
		internal void OnRoomPlayerExit(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRoomPlayerExit:{0}",id);
			onRoomPlayerExit.Value = id;
		}
		
		[SerializeField]public NodeBool isMasterClient;
		internal void OnMasterClientSwitched(string id)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnMasterClientSwitched:{0}",id);
			
			string tempId = MnPlayer.CurrentId;
			MnPlayer.CurrentId = id;
			
			isMasterClient.Value = MnPlayer.IsMy;
			
			MnPlayer.CurrentId = tempId;
		}
		#endregion
	}
}
#endif
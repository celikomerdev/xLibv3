#if xLibv2
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xLib.xNew
{
	//TODO clean
	public class MnArena : SingletonM<MnArena>
	{
		public int startTime=3;
		public NodeBool isAction;
		public bool isStarted;
		public bool isFinished;
		public bool isWinner;
		
		[NonSerialized]public View myAvatar;
		[NonSerialized]public View[] onlineAvatars = new View[0];
		
		
		#region ArenaPlayers
		public void ResetRound()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ResetRound");
			NewOnlineAvatars();
			//MnClient.ins.SpawnMyAvatar();
		}
		
		public void NewOnlineAvatars()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":NewOnlineAvatars");
			isStarted = false;
			isFinished = false;
			isAction.Value = false;
			DestroyAvatarAll();
			onlineAvatars = new View[MnRoomPlayer.ins.playerCount.Value];
		}
		#endregion
		
		
		[SerializeField]private GameObject avatarBase;
		#region Spawning
		public NodeInt onSpawnAvatar;
		public NodeVoid onSpawnAvatarMy;
		public void SpawnAvatar()
		{
			string currentId = MnPlayer.CurrentId;
			if(CanDebug) Debug.LogFormat(this,this.name+":SpawnAvatar:{0}",currentId);
			
			//SpawnPoint
			if(SpawnerMulti.ins == null) return;
			if(SpawnerMulti.ins.transform == null) return;
			Transform spawnPoint = SpawnerMulti.ins.ParentTransform(MnRoomPlayer.ins.playerIndexId.Value,MnRoomPlayer.ins.playerCount.Value).transform;
			
			
			//SpaceSpawning
			avatarBase.SetActive(false);
			GameObject tempObj = Instantiate(avatarBase) as GameObject;
			tempObj.transform.SetParent(spawnPoint);
			tempObj.transform.ResetTransform();
			tempObj.transform.SetParent(null);
			
			
			//TransferOwner
			#if Photon
			Photon.Pun.PhotonView view = tempObj.GetComponent<Photon.Pun.PhotonView>();
			//PhotonNetwork.UnAllocateViewID(tempView.viewID);
			view.ViewID = int.Parse(currentId)*10;
			view.TransferOwnership(int.Parse(currentId));
			#endif
			
			
			//AvatarAssing
			onlineAvatars[MnRoomPlayer.ins.playerIndexId.Value] = tempObj.GetComponent<View>();
			onlineAvatars[MnRoomPlayer.ins.playerIndexId.Value].Id = currentId;
			tempObj.SetActive(true);
			
			
			//Callbacks
			onSpawnAvatar.Value = int.Parse(currentId); //TODO string
			
			if(MnPlayer.IsMy)
			{
				myAvatar = onlineAvatars[MnRoomPlayer.ins.playerIndexId.Value];
				onSpawnAvatarMy.Call();
			}
		}
		#endregion
		
		
		public void DestroyAvatarPlayer()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":DestroyAvatarPlayer");
			Destroy(onlineAvatars[MnRoomPlayer.ins.playerIndexId.Value].gameObject);
		}
		
		
		private void DestroyAvatarAll()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":DestroyAvatarAll");
			for (int i = 0; i < onlineAvatars.Length; i++)
			{
				View tempView = onlineAvatars[i];
				if(!tempView) continue;
				
				GameObject tempGameObject = tempView.gameObject;
				if(CanDebug) Debug.LogWarningFormat(tempGameObject,tempGameObject.name+":DestroyAvatar:{0}",i);
				Destroy(tempGameObject);
			}
		}
		
		
		#region RoundStarter
		public NodeVoid onRoundStarter;
		public void RoundStarter()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":RoundStarter");
			
			if(inCounter) return;
			inCounter = true;
			
			cStartCounter = MnCoroutine.ins.WaitForSecondsRealtime(startTime,RoundStarted);
			cStartCounterUI = MnCoroutine.ins.NewCoroutine(eStartCounterUI());
			
			onRoundStarter.Call();
		}
		
		public void RoundStarterStop()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":RoundStarterStop");
			
			if(!inCounter) return;
			inCounter = false;
			
			MnCoroutine.ins.StopCoroutine(cStartCounter);
			MnCoroutine.ins.StopCoroutine(cStartCounterUI);
		}
		
		public NodeInt onStartCounterUpdate;
		private IEnumerator eStartCounterUI()
		{
			int tempStartTime = startTime;
			while(tempStartTime>0)
			{
				onStartCounterUpdate.Value = tempStartTime;
				tempStartTime--;
				yield return new WaitForSecondsRealtime(1);
			}
			yield return new WaitForEndOfFrame();
		}
		
		private bool inCounter;
		private Coroutine cStartCounter;
		private Coroutine cStartCounterUI;
		#endregion
		
		
		
		#region RoundResult
		public NodeVoid onRoundStarted;
		private void RoundStarted()
		{
			RoundStarterStop();
			if(CanDebug) Debug.LogFormat(this,this.name+":RoundStarted");
			
			if(isStarted) return;
			isStarted = true;
			isAction.Value = true;
			onRoundStarted.Call();
		}
		
		public NodeVoid onRoundEnded;
		public void RoundEnded(bool winner)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":RoundEnded:{0}",isWinner);
			if(!isStarted) return;
			if(isFinished) return;
			isFinished = true;
			isAction.Value = false;
			isWinner = winner;
			onRoundEnded.Call();
		}
		
		public NodeVoid onLeaveArena;
		public void LeaveArena()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LeaveArena");
			//MnRoom.ins.LeaveRoom();
			onLeaveArena.Call();
			isAction.Value = false;
		}
		#endregion
	}
}
#endif
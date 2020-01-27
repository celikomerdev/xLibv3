#if xLibv2
using System.Collections;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xNew
{
	//TODO clean
	public class MnRoom : SingletonM<MnRoom>
	{
		public NodeBool isLock;
		public NodeBool inWait;
		public NodeFloat timeWait;
		
		#region Waiting
		public void SetWait(bool value)
		{
			if(inWait.Value == value) return;
			inWait.Value = value;
			
			if(value) cWaitPlayers = MnCoroutine.ins.NewCoroutine(eWaitPlayers());
			else if(cWaitPlayers!=null) MnCoroutine.ins.StopCoroutine(cWaitPlayers);
		}
		
		private Coroutine cWaitPlayers;
		private IEnumerator eWaitPlayers()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":eWaitPlayers");
			while (timeWait.Value>0)
			{
				timeWait.Value--;
				yield return new WaitForSecondsRealtime(1);
			}
			timeWait.Value = 0;
			
			TryLockRoom();
			yield return new WaitForEndOfFrame();
		}
		#endregion
		
		
		#region Lock
		public void TryLockRoom()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":TryLockRoom");
			if(!MnMulti.ins.inRoom.Value) return;
			
			if(MnNew.ins.maxPlayers.Value==1 || MnNew.ins.maxPlayers.Value==MnRoomPlayer.ins.playerCount.Value)
			{
				isLock.Value = true;
			}
			else if(timeWait.Value==0)
			{
				if(MnRoomPlayer.ins.playerCount.Value>1)
				{
					isLock.Value = true;
				}
				else
				{
					if(CanDebug) Debug.LogWarning(this.name+" STILLWAITINGFOR2!",this);
				}
			}
		}
		#endregion
	}
}
#endif
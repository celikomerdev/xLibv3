#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib.xNew
{
	public class CooldownLoop : BaseTickNodeM
	{
		[Header("Target")]
		[SerializeField]private long cooldown = 600000000;
		[SerializeField]private NodeLong currentTime = null;
		[SerializeField]private NodeLong nextTime = null;
		
		[Header("Work")]
		[SerializeField]private int maxLoop = 1;
		[SerializeField]private EventUnity eventLoop = new EventUnity();
		[SerializeField]private EventBool eventReady = new EventBool();
		
		private long deltaTick = 0;
		[SerializeField]private EventLong eventDeltaTick = new EventLong();
		
		
		protected override void Tick(float tickTime)
		{
			Call();
			eventDeltaTick.Invoke(deltaTick);
		}
		
		public void Call()
		{
			if(!CanWork) return;
			if(currentTime.Value == 0) return;
			
			deltaTick = nextTime.Value-currentTime.Value;
			if(CanDebug) Debug.LogFormat(this,this.name+":deltaTick:{0}",deltaTick);
			
			if(deltaTick>0)
			{
				eventReady.Invoke(false);
				return;
			}
			
			if(deltaTick>-cooldown)
			{
				if(CanDebug) Debug.LogFormat(this,this.name+":NoLoop:{0}",deltaTick);
			}
			else
			{
				int currentLoop = 0;
				while(currentTime.Value > nextTime.Value)
				{
					currentLoop++;
					if(currentLoop < maxLoop) nextTime.Value += cooldown;
					else nextTime.Value = currentTime.Value;
					
					if(CanDebug) Debug.LogFormat(this,this.name+":Reward:{0}",currentLoop);
					eventLoop.Invoke();
				}
				if(nextTime.Value>currentTime.Value) nextTime.Value = currentTime.Value;
			}
			
			eventReady.Invoke(true);
		}
		
		public void StartCooldown()
		{
			nextTime.Value = currentTime.Value+cooldown;
		}
		
		public void TickAdd(long value)
		{
			nextTime.Value += value;
		}
		
		public void TickMultiply(float value)
		{
			long deltaTick = nextTime.Value - currentTime.Value;
			deltaTick = (long)(deltaTick*value);
			TickAdd(deltaTick);
		}
	}
}
#endif
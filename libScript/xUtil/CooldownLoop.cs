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
		public long Cooldown
		{
			get
			{
				return cooldown;
			}
			set
			{
				cooldown = value;
				this.enabled = true;
				Call();
			}
		}
		[SerializeField]public NodeLong currentTime = null;
		[SerializeField]public NodeLong nextTime = null;
		
		[Header("Work")]
		[SerializeField]private int maxLoop = 1;
		[SerializeField]private EventUnity eventLoop = new EventUnity();
		[SerializeField]private EventLong eventDeltaTick = new EventLong();
		
		
		protected override void Tick(float tickTime)
		{
			Call();
		}
		
		public void Call()
		{
			if(!CanWork) return;
			if(currentTime.Value == 0) return;
			
			long deltaTick = nextTime.Value-currentTime.Value;
			if(CanDebug) Debug.LogFormat(this,this.name+":deltaTick:{0}",deltaTick);
			
			if(deltaTick>0)
			{
				eventDeltaTick.Invoke(deltaTick);
				return;
			}
			this.enabled = false;
			
			int currentLoop = 0;
			while(currentTime.Value > nextTime.Value)
			{
				currentLoop++;
				if(currentLoop < maxLoop) nextTime.Value += cooldown;
				else nextTime.Value = currentTime.Value+cooldown;
				
				if(CanDebug) Debug.LogFormat(this,this.name+":Reward:{0}",currentLoop);
				eventLoop.Invoke();
			}
		}
		
		public void CooldownReStart()
		{
			nextTime.Value = currentTime.Value+cooldown;
			this.enabled = true;
			Call();
		}
		
		public void TickAdd(long value)
		{
			nextTime.Value += value;
			this.enabled = true;
			Call();
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
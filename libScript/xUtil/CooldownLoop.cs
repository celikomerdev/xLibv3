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
			}
		}
		[SerializeField]public NodeLong currentTime = null;
		[SerializeField]public NodeLong nextTime = null;
		
		[Header("Work")]
		[SerializeField]private int maxLoop = 1;
		[SerializeField]private EventUnity eventLoop = new EventUnity();
		[SerializeField]private EventInt eventLoopCount = new EventInt();
		
		
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
			
			if(deltaTick>0) return;
			
			int currentLoop = 0;
			while(currentTime.Value > nextTime.Value)
			{
				currentLoop++;
				if(currentLoop < maxLoop) nextTime.Value += cooldown;
				else nextTime.Value = currentTime.Value;
				// else nextTime.Value = currentTime.Value+cooldown;
				
				if(CanDebug) Debug.LogFormat(this,this.name+":eventLoop:{0}",currentLoop);
				eventLoop.Invoke();
			}
			if(CanDebug) Debug.LogFormat(this,this.name+":eventLoopCount:{0}",currentLoop);
			eventLoopCount.Value = currentLoop;
		}
		
		public void CooldownReStart()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":CooldownReStart");
			nextTime.Value = currentTime.Value+cooldown;
		}
		
		public void CooldownStart()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":CooldownStart");
			if(nextTime.Value>currentTime.Value) return;
			CooldownReStart();
		}
		
		public void TickAdd(long value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":TickAdd:{0}",value);
			nextTime.Value += value;
		}
		
		public void TickMultiply(float value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":TickMultiply:{0}",value);
			long deltaTick = nextTime.Value - currentTime.Value;
			deltaTick = (long)(deltaTick*value);
			TickAdd(deltaTick);
		}
	}
}
#endif
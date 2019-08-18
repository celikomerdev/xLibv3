#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnCooldown : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("lastTime")]
		[SerializeField]private float timeLast = 0;
		
		[UnityEngine.Serialization.FormerlySerializedAs("intervalTime")]
		[SerializeField]private float timeInterval = 1;
		
		[SerializeField]private EventFloat eventTimeRemaining = new EventFloat();
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventIsReady = new EventBool();
		
		
		public void Call()
		{
			if(!CanWork) return;
			eventIsReady.Invoke(IsReady);
		}
		
		private bool IsReady
		{
			get
			{
				float timeRemaining = (timeLast+timeInterval)-Time.realtimeSinceStartup;
				if(timeRemaining > 0)
				{
					eventTimeRemaining.Invoke(timeRemaining);
					return false;
				}
				timeLast = Time.realtimeSinceStartup;
				return true;
			}
		}
	}
}
#endif
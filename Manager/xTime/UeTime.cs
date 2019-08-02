#if xLibv1
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class UeTime : SingletonM<UeTime>
	{
		[Header("Manager")]
		public NodeBool timePause;
		public NodeFloat timeScale;
		public NodeFloat timeScaleCache;
		
		public float TimeScale
		{
			get
			{
				return timeScale.Value;
			}
			set
			{
				if(timeScale.Value>0.002f) timeScaleCache.Value = timeScale.Value;
				timeScale.Value = value;
				Time.timeScale = value;
				timePause.Value = (value<=0.002f);
			}
		}
		
		public bool TimePause
		{
			get
			{
				return timePause.Value;
			}
			set
			{
				timePause.Value = value;
				timeScale.Value = (value? 0.002f:timeScaleCache.Value);
			}
		}
	}
}
#endif
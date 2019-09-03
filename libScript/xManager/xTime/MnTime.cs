#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnTime : SingletonM<MnTime>
	{
		[Header("Manager")]
		[SerializeField]private NodeBool timePause = null;
		[SerializeField]private NodeFloat timeScale = null;
		[SerializeField]private NodeFloat timeScaleCache = null;
		
		public float TimeScale
		{
			get
			{
				return timeScale.Value;
			}
			set
			{
				if(timeScale.Value!=0) timeScaleCache.Value = timeScale.Value;
				timeScale.Value = value;
				timePause.Value = (value==0);
				
				Time.timeScale = Mathf.Lerp(0.001f,1f,value);
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
				timeScale.Value = (value? 0:timeScaleCache.Value);
			}
		}
	}
}
#endif
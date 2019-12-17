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
				return StTime.timeScale;
			}
			set
			{
				if(StTime.timeScale == value) return;
				if(timeScale.Value!=0) timeScaleCache.Value = StTime.timeScale;
				StTime.timeScale = value;
				timeScale.Value = value;
				TimePause = (value==0);
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
				if(timePause.Value == value) return;
				timePause.Value = value;
				TimeScale = (value? 0:timeScaleCache.Value);
			}
		}
	}
	
	public static class StTime
	{
		private static float m_timeScale = 1f;
		public static float timeScale
		{
			get
			{
				return m_timeScale;
			}
			set
			{
				if(m_timeScale == value) return;
				m_timeScale = value;
				
				value = Mathf.LerpUnclamped(0.001f,1f,value);
				if(xDebug.CanDebug) Debug.LogFormat("Time.timeScale:{0}:{1}",Time.timeScale,value);
				Time.timeScale = value;
			}
		}
	}
}
#endif
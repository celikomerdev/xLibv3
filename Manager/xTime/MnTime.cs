#if xLibv2
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnTime : SingletonM<MnTime>
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
				if(timeScale.Value!=0) timeScaleCache.Value = timeScale.Value;
				timeScale.Value = value;
				timePause.Value = (value==0);
				
				//TODO dont change Time.timeScale!
				Time.timeScale = Mathf.Lerp(0.002f,1f,value);
				//Time.timeScale = value;
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
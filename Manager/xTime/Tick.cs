﻿#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xTick
{
	public class Tick : BaseWorkM
	{
		private static float timeScale = 1;
		
		[Header("Update")]
		[SerializeField]private NodeFloat tickUpdate;
		[SerializeField]private NodeFloat tickUnscaledUpdate;
		
		[Header("Smooth")]
		[SerializeField]private NodeFloat tickSmooth;
		[SerializeField]private NodeFloat tickUnscaledSmooth;
		
		[Header("Late")]
		[SerializeField]private NodeFloat tickLate;
		[SerializeField]private NodeFloat tickUnscaledLate;
		
		[Header("Fixed")]
		[SerializeField]private NodeFloat tickFixed;
		[SerializeField]private NodeFloat tickUnscaledFixed;
		
		[Header("Start")]
		[SerializeField]private NodeFloat tickStart;
		[SerializeField]private NodeFloat tickUnscaledStart;
		
		[Header("Level")]
		[SerializeField]private NodeFloat tickLevel;
		[SerializeField]private NodeFloat tickUnscaledLevel;
		
		[Header("Interval")]
		[SerializeField]private TickIntervalGroup tickIntervalGroup;
		
		private void Start()
		{
			if(!CanWork) return;
			tickIntervalGroup.IntervalStart(this);
		}
		
		private void Update()
		{
			if(!CanWork) return;
			if(Time.unscaledDeltaTime == 0) return;
			
			string lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			
			tickUnscaledUpdate.Value = Time.unscaledDeltaTime;
			tickUnscaledSmooth.Value = Mathf.Lerp(tickUnscaledSmooth.Value,tickUnscaledUpdate.Value,0.5f);
			tickUnscaledStart.Value += tickUnscaledUpdate.Value;
			tickUnscaledLevel.Value += tickUnscaledUpdate.Value;
			
			if(timeScale != 0)
			{
				tickUpdate.Value = timeScale*tickUnscaledUpdate.Value;
				tickSmooth.Value = timeScale*tickUnscaledSmooth.Value;
				tickStart.Value += tickUpdate.Value;
				tickLevel.Value += tickUpdate.Value;
			}
			
			ViewCore.CurrentId = lastViewId;
		}
		
		private void LateUpdate()
		{
			if(!CanWork) return;
			if(Time.unscaledDeltaTime == 0) return;
			
			string lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			
			tickUnscaledLate.Value = Time.unscaledDeltaTime;
			
			if(timeScale != 0)
			{
				tickLate.Value = timeScale*tickUnscaledLate.Value;
			}
			
			ViewCore.CurrentId = lastViewId;
		}
		
		private void FixedUpdate()
		{
			if(!CanWork) return;
			if(Time.fixedUnscaledDeltaTime == 0) return;
			
			string lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			
			tickUnscaledFixed.Value = Time.fixedUnscaledDeltaTime;
			
			if(timeScale == 0)
			{
				tickFixed.Value = Time.fixedDeltaTime;
			}
			
			ViewCore.CurrentId = lastViewId;
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xTick
{
	public class Tick : BaseWorkM
	{
		private static float timeScale = 1;
		
		[Header("Update")]
		[SerializeField]private NodeFloat tickUpdate = null;
		[SerializeField]private NodeFloat tickUnscaledUpdate = null;
		
		[Header("Smooth")]
		[SerializeField]private NodeFloat tickSmooth = null;
		[SerializeField]private NodeFloat tickUnscaledSmooth = null;
		
		[Header("Late")]
		[SerializeField]private NodeFloat tickLate = null;
		[SerializeField]private NodeFloat tickUnscaledLate = null;
		
		[Header("Fixed")]
		[SerializeField]private NodeFloat tickFixed = null;
		[SerializeField]private NodeFloat tickUnscaledFixed = null;
		
		[Header("Start")]
		[SerializeField]private NodeFloat tickStart = null;
		[SerializeField]private NodeFloat tickUnscaledStart = null;
		
		[Header("Level")]
		[SerializeField]private NodeFloat tickLevel = null;
		[SerializeField]private NodeFloat tickUnscaledLevel = null;
		
		[Header("Interval")]
		[SerializeField]private TickIntervalGroup tickIntervalGroup = new TickIntervalGroup();
		
		private void Start()
		{
			if(!CanWork) return;
			tickIntervalGroup.IntervalStart(this);
		}
		
		private void Update()
		{
			if(!CanWork) return;
			if(Time.unscaledDeltaTime == 0) return;
			
			string tempViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = string.Empty;
			
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
			
			ViewCore.CurrentId = tempViewId;
		}
		
		private void LateUpdate()
		{
			if(!CanWork) return;
			if(Time.unscaledDeltaTime == 0) return;
			
			string tempViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = string.Empty;
			
			tickUnscaledLate.Value = Time.unscaledDeltaTime;
			
			if(timeScale != 0)
			{
				tickLate.Value = timeScale*tickUnscaledLate.Value;
			}
			
			ViewCore.CurrentId = tempViewId;
		}
		
		private void FixedUpdate()
		{
			if(!CanWork) return;
			if(Time.fixedUnscaledDeltaTime == 0) return;
			
			string tempViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = string.Empty;
			
			tickUnscaledFixed.Value = Time.fixedUnscaledDeltaTime;
			
			if(timeScale == 0)
			{
				tickFixed.Value = Time.fixedDeltaTime;
			}
			
			ViewCore.CurrentId = tempViewId;
		}
	}
}
#endif
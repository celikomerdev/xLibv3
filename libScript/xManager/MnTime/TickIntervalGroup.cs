#if xLibv3
using System;
using System.Collections;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xTick
{
	[Serializable]public class TickIntervalGroup
	{
		private BaseWorkM mono = null;
		[SerializeField]private TickInterval[] tickInterval = new TickInterval[0];
		[Serializable]public class TickInterval
		{
			[SerializeField]internal bool unscaled = true;
			[SerializeField]internal float interval = 1;
			[SerializeField]private NodeFloat tickInterval = null;
			
			internal IEnumerator Tick()
			{
				string lastViewId = ViewCore.CurrentId;
				ViewCore.CurrentId = "Client";
			
				tickInterval.Value = interval;
				
				ViewCore.CurrentId = lastViewId;
				yield return new WaitForEndOfFrame();
			}
		}
		
		private IEnumerator IntervalUpdate(TickInterval value)
		{
			while(true)
			{
				if(value.unscaled) yield return new WaitForSecondsRealtime(value.interval);
				else yield return new WaitForSeconds(value.interval);
				
				yield return new WaitForEndOfFrame();
				yield return mono.NewCoroutine(value.Tick(),mono.CanDebug);
			}
		}
		
		internal void IntervalStart(BaseWorkM monoBehaviour)
		{
			mono = monoBehaviour;
			for (int i = 0; i < tickInterval.Length; i++)
			{
				mono.NewCoroutine(IntervalUpdate(tickInterval[i]),mono.CanDebug);
			}
		}
	}
}
#endif
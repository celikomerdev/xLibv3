#if xLibv3
using System;
using System.Collections;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xTick
{
	[Serializable]public class TickIntervalGroup
	{
		private MonoBehaviour mono;
		[SerializeField]private TickInterval[] tickInterval;
		[Serializable]public class TickInterval
		{
			[SerializeField]internal bool unscaled;
			[SerializeField]internal float interval;
			[SerializeField]private NodeFloat tickInterval;
			
			internal IEnumerator Tick()
			{
				string lastViewId = ViewCore.CurrentId;
				ViewCore.CurrentId = "Client";
			
				tickInterval.Value = interval;
				
				ViewCore.CurrentId = lastViewId;
				yield return null;
			}
		}
		
		private IEnumerator IntervalUpdate(TickInterval value)
		{
			while(true)
			{
				if(value.unscaled) yield return new WaitForSecondsRealtime(value.interval);
				else yield return new WaitForSeconds(value.interval);
				
				yield return new WaitForEndOfFrame();
				yield return mono.StartCoroutine(value.Tick());
			}
		}
		
		internal void IntervalStart(BaseM monoBehaviour)
		{
			mono = monoBehaviour;
			for (int i = 0; i < tickInterval.Length; i++)
			{
				mono.StartCoroutine(IntervalUpdate(tickInterval[i]));
			}
		}
	}
}
#endif
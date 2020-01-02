#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public class MnAction : SingletonM<MnAction>
	{
		public static List<UnityAction> listAction = new List<UnityAction>();
		
		public void CallActionNextStatic(float delay = 0)
		{
			CallActionNext(delay);
		}
		
		public static void CallActionNext(float delay = 0)
		{
			xDebug.LogFormat("MnAction.CallActionNext");
			MnCoroutine.ins.NewCoroutine(callActionNext(delay));
		}
		
		private static float lastTime = 0;
		private static IEnumerator callActionNext(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			
			if(listAction.Count==0) yield break;
			if(0.5f>Time.unscaledTime-lastTime) yield break;
			lastTime = Time.unscaledTime;
			
			UnityAction tempAction = listAction[0];
			listAction.RemoveAt(0);
			tempAction.Invoke();
		}
	}
}
#endif
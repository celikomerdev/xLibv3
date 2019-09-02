#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public class MnAction : SingletonM<MnAction>
	{
		public static List<UnityAction> customAction = new List<UnityAction>();
		
		public static void CallNextAction()
		{
			MnCoroutine.NewCoroutine(callNextAction());
		}
		
		private static IEnumerator callNextAction()
		{
			yield return new WaitForSecondsRealtime(0.5f);
			
			if(customAction.Count>0)
			{
				customAction[0]();
				customAction.RemoveAt(0);
			}
		}
	}
}
#endif
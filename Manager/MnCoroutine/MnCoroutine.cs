#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public class MnCoroutine : SingletonM<MnCoroutine>
	{
		#region NewCoroutine
		public Coroutine NewCoroutine(IEnumerator routine)
		{
			if(gameObject.activeInHierarchy) return StartCoroutine(routine);
			
			Debug.LogWarningFormat(this,this.name+":NewCoroutine:Disabled");
			return null;
		}
		#endregion
		
		
		#region Scaled
		public Coroutine WaitForSeconds(float delay,UnityAction call)
		{
			return NewCoroutine(eWaitForSeconds(delay,call));
		}
		
		private IEnumerator eWaitForSeconds(float delay,UnityAction call)
		{
			yield return new WaitForSeconds(delay);
			call();
			yield return null;
		}
		#endregion
		
		#region Unscaled
		public Coroutine WaitForSecondsRealtime(float delay,UnityAction call)
		{
			return NewCoroutine(eWaitForSecondsRealtime(delay,call));
		}
		
		private IEnumerator eWaitForSecondsRealtime(float delay,UnityAction call)
		{
			yield return new WaitForSecondsRealtime(delay);
			call();
			yield return null;
		}
		#endregion
	}
}
#endif
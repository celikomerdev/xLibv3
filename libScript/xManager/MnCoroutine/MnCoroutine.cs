#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public class MnCoroutine : SingletonM<MnCoroutine>
	{
		#region NewCoroutine
		public static void KillCoroutine(Coroutine coroutine)
		{
			if(!ins)
			{
				xDebug.LogExceptionFormat("MnCoroutine.ins:null");
				return;
			}
			
			if(coroutine == null) return;
			ins.StopCoroutine(coroutine);
		}
		
		public static Coroutine NewCoroutine(IEnumerator enumerator)
		{
			if(!ins)
			{
				xDebug.LogExceptionFormat("MnCoroutine.ins:null");
				return null;
			}
			
			return ins.newCoroutine(enumerator);
		}
		
		private Coroutine newCoroutine(IEnumerator enumerator)
		{
			if(!gameObject.activeInHierarchy)
			{
				xDebug.LogExceptionFormat(this,this.name+":!gameObject.activeInHierarchy");
				return null;
			}
			
			if(CanDebug) Debug.LogFormat(this,this.name+":newCoroutine:{0}",enumerator.ToString());
			return StartCoroutine(enumerator);
		}
		#endregion
		
		#region Scaled
		public static Coroutine WaitForSeconds(float delay,UnityAction call)
		{
			return NewCoroutine(waitForSeconds(delay,call));
		}
		
		private static IEnumerator waitForSeconds(float delay,UnityAction call)
		{
			yield return new WaitForSeconds(delay);
			call();
		}
		#endregion
		
		#region Unscaled
		public static Coroutine WaitForSecondsRealtime(float delay,UnityAction call)
		{
			return NewCoroutine(waitForSecondsRealtime(delay,call));
		}
		
		private static IEnumerator waitForSecondsRealtime(float delay,UnityAction call)
		{
			yield return new WaitForSecondsRealtime(delay);
			call();
		}
		#endregion
	}
}
#endif
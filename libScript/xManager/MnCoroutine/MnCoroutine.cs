#if xLibv3
using System;
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
		
		#region WaitForSeconds
		public static Coroutine WaitForSeconds(UnityAction call,float delay=0,bool unscaled=true)
		{
			return NewCoroutine(waitForSeconds(call,delay,unscaled));
		}
		
		private static IEnumerator waitForSeconds(UnityAction call,float delay=0,bool unscaled=true)
		{
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			call();
		}
		#endregion
		
		#region WaitUntil
		public static Coroutine WaitUntil(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			return NewCoroutine(waitUntil(predicate,call,delay,unscaled));
		}
		
		private static IEnumerator waitUntil(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			yield return new WaitUntil(predicate);
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			call();
		}
		#endregion
		
		#region WaitWhile
		public static Coroutine WaitWhile(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			return NewCoroutine(waitWhile(predicate,call,delay,unscaled));
		}
		
		private static IEnumerator waitWhile(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			yield return new WaitWhile(predicate);
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			call();
		}
		#endregion
	}
}
#endif
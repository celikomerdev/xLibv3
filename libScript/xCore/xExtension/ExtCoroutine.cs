#if xLibv3
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

namespace xLib
{
	public static class ExtCoroutine
	{
		#region NewCoroutine
		public static void KillCoroutine(this MonoBehaviour mono,Coroutine coroutine)
		{
			if(!mono)
			{
				Debug.LogException(new UnityException($"{mono.name}:KillCoroutine:mono:null"),mono);
				return;
			}
			
			if(coroutine == null) return;
			mono.StopCoroutine(coroutine);
		}
		
		public static Coroutine NewCoroutine(this MonoBehaviour mono,IEnumerator enumerator,bool CanDebug = false)
		{
			if(!mono)
			{
				Debug.LogException(new UnityException($"{mono.name}:NewCoroutine:mono:null"),mono);
				return null;
			}
			
			return newCoroutine(mono,enumerator,CanDebug);
		}
		
		private static Coroutine newCoroutine(MonoBehaviour mono,IEnumerator enumerator,bool CanDebug)
		{
			if(!mono.isActiveAndEnabled)
			{
				Debug.LogException(new UnityException($"{mono.name}:!isActiveAndEnabled"),mono);
				return null;
			}
			
			// if(CanDebug) Debug.Log($"{mono.name}:newCoroutine:{enumerator.ToString()}",mono);
			return mono.StartCoroutine(enumerator);
		}
		#endregion
		
		#region WaitForSeconds
		public static Coroutine WaitForSeconds(this MonoBehaviour mono,UnityAction call,float delay=0,bool unscaled=true)
		{
			return mono.NewCoroutine(waitForSeconds(call,delay,unscaled));
		}
		
		private static IEnumerator waitForSeconds(UnityAction call,float delay=0,bool unscaled=true)
		{
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			Profiler.BeginSample($"{call.Method.Name}:waitForSeconds:{delay}");
			call();
			Profiler.EndSample();
		}
		#endregion
		
		#region WaitUntil
		public static Coroutine WaitUntil(this MonoBehaviour mono,Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			return mono.NewCoroutine(waitUntil(predicate,call,delay,unscaled));
		}
		
		private static IEnumerator waitUntil(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			yield return new WaitUntil(predicate);
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			Profiler.BeginSample($"{call.Method.Name}:waitUntil:{delay}");
			call();
			Profiler.EndSample();
		}
		#endregion
		
		#region WaitWhile
		public static Coroutine WaitWhile(this MonoBehaviour mono,Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			return mono.NewCoroutine(waitWhile(predicate,call,delay,unscaled));
		}
		
		private static IEnumerator waitWhile(Func<bool> predicate,UnityAction call,float delay=0,bool unscaled=true)
		{
			yield return new WaitWhile(predicate);
			if(unscaled)yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			Profiler.BeginSample($"{call.Method.Name}:waitWhile:{delay}");
			call();
			Profiler.EndSample();
		}
		#endregion
	}
}
#endif
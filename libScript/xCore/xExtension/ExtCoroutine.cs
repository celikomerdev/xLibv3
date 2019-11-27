﻿#if xLibv3
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class ExtCoroutine
	{
		#region NewCoroutine
		public static void KillCoroutine(this MonoBehaviour mono,Coroutine coroutine)
		{
			if(!mono)
			{
				xDebug.LogExceptionFormat("KillCoroutine:mono:null");
				return;
			}
			
			if(coroutine == null) return;
			mono.StopCoroutine(coroutine);
		}
		
		public static Coroutine NewCoroutine(this MonoBehaviour mono,IEnumerator enumerator)
		{
			if(!mono)
			{
				xDebug.LogExceptionFormat("NewCoroutine:mono:null");
				return null;
			}
			
			return newCoroutine(mono,enumerator);
		}
		
		private static Coroutine newCoroutine(MonoBehaviour mono,IEnumerator enumerator)
		{
			if(!mono.isActiveAndEnabled)
			{
				xDebug.LogExceptionFormat(mono,mono.name+":!isActiveAndEnabled");
				return null;
			}
			
			// if(xDebug.CanDebug) Debug.LogFormat(mono,mono.name+":newCoroutine:{0}",enumerator.ToString());
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
			call();
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
			call();
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
			call();
		}
		#endregion
	}
}
#endif
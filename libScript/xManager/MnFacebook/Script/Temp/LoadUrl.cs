#if xLibv2
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class LoadUrl
	{
		public static void Load(string url, UnityAction<WWW> call)
		{
			MnCoroutine.ins.NewCoroutine(eLoad(url,call));
		}
		
		private static IEnumerator eLoad(string url, UnityAction<WWW> call)
		{
			if(string.IsNullOrEmpty(url)) yield break;
			
			WWW www = new WWW(url);
			yield return www;
			
			call.Invoke(www);
			
			www.Dispose();
			www = null;
			yield return null;
		}
	}
}
#endif
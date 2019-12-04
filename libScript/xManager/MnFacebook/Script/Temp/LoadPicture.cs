#if xLibv2
using System;
using System.Collections;
using UnityEngine;

namespace xLib
{
	public static class LoadPicture
	{
		public static void Load(string url)
		{
			MnCoroutine.ins.NewCoroutine(eLoad(url));
		}
		
		private static IEnumerator eLoad(string url)
		{
			if(string.IsNullOrEmpty(url)) yield break;
			
			WWW www = new WWW(url);
			yield return www;
			
			if (string.IsNullOrEmpty(www.error))
			{
				//callback(www.texture);
			}
			else
			{
				xDebug.LogExceptionFormat("LoadPicture:error:{0}:{1}",www.error,url);
			}
			www.Dispose();
			www = null;
			yield return null;
		}
	}
}
#endif
#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class ExtWWW
	{
		public static Coroutine WwwYield(this BaseWorkerI worker,UnityAction<WWW> call,string url)
		{
			return MnCoroutine.ins.NewCoroutine(Flow(),worker.CanDebug);
			IEnumerator Flow()
			{
				WWW www = new WWW(url);
				yield return www;
				
				if(!string.IsNullOrWhiteSpace(www.error))
				{
					Debug.LogException(new UnityException($"{worker.UnityObject.name}:error:{www.error}:url:{url}"),worker.UnityObject);
				}
				else
				{
					string tempViewId = ViewCore.CurrentId;
					if(!worker.IsNull()) ViewCore.CurrentId = worker.ViewId;
					call.Invoke(www);
					ViewCore.CurrentId = tempViewId;
				}
				
				www.Dispose();
				www = null;
			}
		}
	}
}
#endif
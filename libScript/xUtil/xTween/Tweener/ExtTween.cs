#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class ExtTween
	{
		public static Coroutine Tween(UnityAction<float> call,float duration = 0.1f, bool ignoreTimeScale=true)
		{
			return MnCoroutine.ins.NewCoroutine(Flow());
			IEnumerator Flow()
			{
				float elapsedTime = 0.0f;
				while (elapsedTime < duration)
				{
					elapsedTime += ignoreTimeScale? Time.unscaledDeltaTime:Time.deltaTime;
					float ratio = Mathf.Clamp01(elapsedTime/duration);
					call.Invoke(ratio);
					yield return null;
				}
			}
		}
	}
}
#endif
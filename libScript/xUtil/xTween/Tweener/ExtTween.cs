#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public static class ExtTween
	{
		public static Coroutine Tween(UnityAction<float> call,float duration = 0.1f, bool ignoreTimeScale=true,AnimationCurve curve=null)
		{
			if(curve==null) curve = AnimationCurve.Linear(0,0,1,1);
			return MnCoroutine.ins.NewCoroutine(Flow());
			IEnumerator Flow()
			{
				float elapsedTime = 0.0f;
				while (elapsedTime < duration)
				{
					elapsedTime += ignoreTimeScale? Time.unscaledDeltaTime:Time.deltaTime;
					float ratio = Mathf.Clamp01(elapsedTime/duration);
					ratio = curve.Evaluate(ratio);
					call.Invoke(ratio);
					yield return new WaitForEndOfFrame();
				}
			}
		}
		
		public static Coroutine TweenDelta(UnityAction<float> call,float duration = 0.1f, bool ignoreTimeScale=true,AnimationCurve curve=null)
		{
			if(curve==null) curve = AnimationCurve.Linear(0,0,1,1);
			return MnCoroutine.ins.NewCoroutine(Flow());
			IEnumerator Flow()
			{
				float elapsedTime = 0.0f;
				float ratioPrev = 0.0f;
				while (elapsedTime < duration)
				{
					elapsedTime += ignoreTimeScale? Time.unscaledDeltaTime:Time.deltaTime;
					float ratio = Mathf.Clamp01(elapsedTime/duration);
					ratio = curve.Evaluate(ratio);
					call.Invoke((ratio-ratioPrev)*5);
					ratioPrev = ratio;
					yield return new WaitForEndOfFrame();
				}
			}
		}
		
		public static Coroutine TweenLerp(UnityAction<float> call,float duration = 0.1f, bool ignoreTimeScale=true,AnimationCurve curve=null)
		{
			if(curve==null) curve = AnimationCurve.Linear(0,0,1,1);
			return MnCoroutine.ins.NewCoroutine(Flow());
			IEnumerator Flow()
			{
				float elapsedTime = 0.0f;
				while (elapsedTime < duration)
				{
					elapsedTime += ignoreTimeScale? Time.unscaledDeltaTime:Time.deltaTime;
					float ratio = Mathf.Clamp01(elapsedTime/duration);
					ratio = curve.Evaluate(ratio);
					call.Invoke(ratio*(0.1f/duration));
					yield return new WaitForEndOfFrame();
				}
			}
		}
	}
}
#endif
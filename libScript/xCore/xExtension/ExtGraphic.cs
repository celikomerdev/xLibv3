#if xLibv3
#if PackUI
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace xLib
{
	public static class ExtGraphic
	{
		public static Coroutine TweenColor(this Graphic graphic, Color targetColor, float duration = 0.1f, bool ignoreTimeScale=true, bool useAlpha=true, bool useRGB=false)
		{
			return MnCoroutine.NewCoroutine(Flow());
			
			IEnumerator Flow()
			{
				Color m_StartColor = graphic.color;
				float elapsedTime = 0.0f;
				while (elapsedTime < duration)
				{
					elapsedTime += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
					float percentage = Mathf.Clamp01(elapsedTime/duration);
					graphic.color = Color.Lerp(m_StartColor,targetColor,percentage);
					yield return null;
				}
				graphic.color = targetColor;
			}
		}
	}
}
#endif
#endif
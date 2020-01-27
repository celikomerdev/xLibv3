#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;
using xLib.xTween;

namespace xLib.xUtil
{
	public class TweenerToggle : BaseWorkM
	{
		[SerializeField]private Toggle toggle = null;
		[UnityEngine.Serialization.FormerlySerializedAs("tweenGroup")]
		[SerializeField]private Tween tween = null;
		[SerializeField]private float duration = 0.1f;
		[SerializeField]private bool ignoreTimeScale = true;
		
		private void Start()
		{
			Fill();
			toggle.onValueChanged.AddListener(Work); Work(toggle.isOn);
		}
		
		private void OnDestroy()
		{
			toggle.onValueChanged.RemoveListener(Work);
		}
		
		private float currentRatio = 0f;
		private Coroutine m_Coroutine = null;
		private void Work(bool value)
		{
			if(!CanWork) return;
			if(!tween) return;
			
			float valueTarget = 1f;
			if(!toggle.isOn) valueTarget = 0f;
			
			MnCoroutine.ins.KillCoroutine(m_Coroutine);
			m_Coroutine = ExtTween.Tween(duration:duration,ignoreTimeScale:ignoreTimeScale,call:(ratio)=>
			{
				currentRatio = Mathf.Lerp(currentRatio,valueTarget,ratio);
				tween.SetBaseRatio(currentRatio);
			});
		}
		
		[ContextMenu("Fill")]
		private void Fill()
		{
			if(!toggle) toggle = GetComponent<Toggle>();
			if(!toggle) toggle = GetComponentInParent<Toggle>();
			
			if(!tween) tween = GetComponent<Tween>();
			if(!tween) tween = GetComponentInChildren<Tween>();
		}
	}
}
#endif
#endif
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
		[SerializeField]private TweenGroup tweenGroup = null;
		[SerializeField]private float duration = 0.1f;
		[SerializeField]private bool ignoreTimeScale = true;
		
		private void Awake()
		{
			Fill();
			toggle.onValueChanged.AddListener(Work); Work(toggle.isOn);
		}
		
		private void OnDestroy()
		{
			toggle.onValueChanged.RemoveListener(Work);
		}
		
		private Coroutine m_Coroutine = null;
		private void Work(bool value)
		{
			if(!CanWork) return;
			if(!tweenGroup) return;
			
			float valueTarget = 1f;
			if(!toggle.isOn) valueTarget = 0f;
			
			MnCoroutine.KillCoroutine(m_Coroutine);
			m_Coroutine = tweenGroup.CrossFade(valueTarget,duration);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("Fill")]
		private void Fill()
		{
			if(!toggle) toggle = GetComponent<Toggle>();
			if(!toggle) toggle = GetComponentInParent<Toggle>();
			
			if(!tweenGroup) tweenGroup = GetComponent<TweenGroup>();
			if(!tweenGroup) tweenGroup = GetComponentInChildren<TweenGroup>();
		}
		#endif
	}
}
#endif
#endif
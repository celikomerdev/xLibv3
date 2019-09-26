#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xUtil
{
	public class ToggleCrossFade : BaseWorkM
	{
		[SerializeField]private Graphic m_TargetGraphic = null;
		[SerializeField]private float fadeDuration = 0.1f;
		[SerializeField]private bool ignoreTimeScale = true;
		
		private Color m_TargetColor;
		[SerializeField]private Color from = Color.white;
		[SerializeField]private Color to = Color.white;
		
		[SerializeField]private Toggle m_Toggle = null;
		
		private void Awake()
		{
			FillGraphic();
			m_Toggle.onValueChanged.AddListener(OnValueChanged); OnValueChanged(m_Toggle.isOn);
		}
		
		private void OnDestroy()
		{
			m_Toggle.onValueChanged.RemoveListener(OnValueChanged);
		}
		
		private void OnValueChanged(bool result)
		{
			if(m_Toggle.isOn) m_TargetColor = to;
			else m_TargetColor = from;
			Work();
		}
		
		private Coroutine m_Tween = null;
		private void Work()
		{
			if(!CanWork) return;
			if(!m_TargetGraphic) return;
			
			MnCoroutine.KillCoroutine(m_Tween);
			m_Tween = m_TargetGraphic.TweenColor(m_TargetColor,fadeDuration);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("FillGraphic")]
		private void FillGraphic()
		{
			if(!m_TargetGraphic) m_TargetGraphic = GetComponent<Graphic>();
			if(!m_TargetGraphic) m_TargetGraphic = GetComponent<Selectable>().targetGraphic;
			
			if(!m_Toggle) m_Toggle = GetComponent<Toggle>();
			if(!m_Toggle) m_Toggle = GetComponentInParent<Toggle>();
		}
		#endif
	}
}
#endif
#endif
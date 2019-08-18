#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace xLib.xUtil
{
	public class SelectableTint : UIBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler, IPointerExitHandler,ISelectHandler,IDeselectHandler
	{
		[SerializeField]private Graphic m_TargetGraphic = null;
		[SerializeField]private float fadeDuration = 0.1f;
		
		[SerializeField]private Color normalColor = Color.white;
		[SerializeField]private Color selectedColor = Color.white;
		[SerializeField]private Color highlightedColor = Color.white;
		[SerializeField]private Color pressedColor = Color.white;
		private Color TargetColor
		{
			get
			{
				if(isPointerDown) return pressedColor;
				if(isPointerInside) return highlightedColor;
				if(hasSelection) return selectedColor;
				return normalColor;
			}
		}
		
		#region PointerEvents
		private bool isPointerDown;
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			isPointerDown = true;
			Work();
		}
		
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			isPointerDown = false;
			Work();
		}
		
		private bool isPointerInside;
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			isPointerInside = true;
			Work();
		}
		
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			isPointerInside = false;
			Work();
		}
		
		private bool hasSelection;
		public virtual void OnSelect(BaseEventData eventData)
		{
			hasSelection = true;
			Work();
		}
		
		public virtual void OnDeselect(BaseEventData eventData)
		{
			hasSelection = false;
			Work();
		}
		#endregion
		
		
		private void Work()
		{
			if(!IsActive()) return;
			if(!m_TargetGraphic) return;
			Color targetColor = TargetColor;
			m_TargetGraphic.CrossFadeColor(targetColor,fadeDuration,true,true);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("FillGraphic")]
		private void FillGraphic()
		{
			if(!m_TargetGraphic) m_TargetGraphic = GetComponent<Graphic>();
			if(!m_TargetGraphic) m_TargetGraphic = GetComponent<Selectable>().targetGraphic;
		}
		#endif
	}
}
#endif
#endif
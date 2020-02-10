#if xLibv3
#if PackUI
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using xLib.EventClass;

namespace xLib
{
	public class ScrollRectFocus : BaseInitM
	{
		[SerializeField]private ScrollRect scrollRect = null;
		private RectTransform transScrollRect = null;
		private RectTransform transContent = null;
		private RectTransform transViewport = null;
		
		[SerializeField]private RectTransform m_target;
		public RectTransform Target
		{
			get
			{
				return m_target;
			}
			set
			{
				m_target = value;
				Call();
			}
		}
		
		protected override void OnInit(bool init)
		{
			if(init)
			{
				if(!Target) Target = (RectTransform)transform;
				transScrollRect = (RectTransform)scrollRect.transform;
				transContent = scrollRect.content;
				transViewport = scrollRect.viewport;
			}
		}
		
		[SerializeField]private EventVector2 eventNormalizedPosition = new EventVector2();
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.Log($"{this.name}:ScrollRectFocus:Call",this);
			if(!transScrollRect) return;
			Vector3 itemCenterPositionInScroll = GetWorldPointInWidget(transScrollRect, GetWidgetWorldPoint(Target));
			Vector3 targetPositionInScroll = GetWorldPointInWidget(transScrollRect, GetWidgetWorldPoint(transViewport));
			Vector3 difference = targetPositionInScroll - itemCenterPositionInScroll;
			difference.z = 0f;
			
			if (!scrollRect.horizontal) difference.x = 0f;
			if (!scrollRect.vertical) difference.y = 0f;
			
			Vector2 normalizedDifference = new Vector2
			(
				difference.x/(transContent.rect.size.x-transScrollRect.rect.size.x),
				difference.y/(transContent.rect.size.y-transScrollRect.rect.size.y)
			);
			
			Vector2 newNormalizedPosition = scrollRect.normalizedPosition - normalizedDifference;
			if (scrollRect.movementType != ScrollRect.MovementType.Unrestricted)
			{
				newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
				newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);
			}
			
			eventNormalizedPosition.Invoke(newNormalizedPosition);
			
			ExtTween.Tween(duration:0.5f,call:(ratio)=>
			{
				scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition,newNormalizedPosition,ratio);
			});
		}
		
		private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
		{
			return target.InverseTransformPoint(worldPoint);
		}
		
		private Vector3 GetWidgetWorldPoint(RectTransform target)
		{
			Vector3 pivotOffset = new Vector3
			(
				target.rect.size.x*(0.5f-target.pivot.x),
				target.rect.size.y*(0.5f-target.pivot.y),
				0f
			);
			Vector3 localPosition = target.localPosition+pivotOffset;
			return target.parent.TransformPoint(localPosition);
		}
	}
}
#endif
#endif
#if xLibv3
using UnityEngine;
using UnityEngine.UI;
using xLib.EventClass;

namespace xLib
{
	public class ScrollRectFocus : MonoInit
	{
		[SerializeField]private ScrollRect scrollRect;
		private RectTransform transScrollRect;
		private RectTransform transContent;
		private RectTransform transViewport;
		
		[SerializeField]private EventVector2 eventNormalizedPosition;
		
		protected override void OnInit(bool init)
		{
			if(init)
			{
				transScrollRect = (RectTransform)scrollRect.transform;
				transContent = scrollRect.content;
				transViewport = scrollRect.viewport;
			}
		}
		
		private RectTransform m_target;
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
		
		public void Call()
		{
			if(!CanWork) return;
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
		}
		
		private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
		{
			return target.InverseTransformPoint(worldPoint);
		}
		
		private Vector3 GetWidgetWorldPoint(RectTransform target)
		{
			var pivotOffset = new Vector3
			(
				(0.5f-target.pivot.x)*target.rect.size.x,
				(0.5f-target.pivot.y)*target.rect.size.y,
				0f
			);
			Vector3 localPosition = target.localPosition+pivotOffset;
			return target.parent.TransformPoint(localPosition);
		}
	}
}
#endif
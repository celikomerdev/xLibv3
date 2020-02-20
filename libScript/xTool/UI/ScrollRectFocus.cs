#if xLibv3
#if PackUI
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
		
		public float duration = 1f;
		
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
		
		private Coroutine cTween = null;
		[ContextMenu("Call")]
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.Log($"{this.name}:ScrollRectFocus:Call",this);
			if(!transScrollRect) return;
			Vector3 itemCenterPositionInScroll = transScrollRect.InverseTransformPoint(GetWidgetWorldPoint(Target));
			Vector3 targetPositionInScroll = transScrollRect.InverseTransformPoint(GetWidgetWorldPoint(transViewport));
			Vector3 difference = targetPositionInScroll - itemCenterPositionInScroll;
			difference.z = 0f;
			
			if (!scrollRect.horizontal) difference.x = 0f;
			if (!scrollRect.vertical) difference.y = 0f;
			
			Rect rectContent = transContent.rect;
			Rect rectScroll = transScrollRect.rect;
			Vector2 normalizedDifference = new Vector2
			(
				difference.x/(rectContent.size.x-rectScroll.size.x),
				difference.y/(rectContent.size.y-rectScroll.size.y)
			);
			
			// Vector2 valueStart = scrollRect.normalizedPosition;
			Vector2 valueTarget = scrollRect.normalizedPosition - normalizedDifference;
			if (scrollRect.movementType != ScrollRect.MovementType.Unrestricted)
			{
				valueTarget.x = Mathf.Clamp01(valueTarget.x);
				valueTarget.y = Mathf.Clamp01(valueTarget.y);
			}
			
			MnCoroutine.ins.KillCoroutine(cTween);
			cTween = ExtTween.TweenDelta(duration:duration,call:(ratio)=>
			{
				scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition,valueTarget,ratio);
				// eventNormalizedPosition.Invoke(valueTarget);
			});
		}
		
		private static Vector3 GetWidgetWorldPoint(RectTransform target)
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
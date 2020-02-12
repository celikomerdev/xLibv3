#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.EventClass;
using xLib.Mathx;

namespace xLib.xTool.xInput
{
	public class TouchWheel : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField]private float rangeAngle = 480f;
		[SerializeField]private Transform pivot = null;
		[SerializeField]private EventFloat axis = new EventFloat();
		
		#region Pointer
		private Vector2 pivotScreenPos = Vector2.zero;
		void IPointerDownHandler.OnPointerDown(PointerEventData pointer)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnPointerDown",this);
			angleTarget = 0;//*axis.Value;
			Refresh();
			
			pivotScreenPos = pointer.pressEventCamera.WorldToScreenPoint(pivot.position);
			anglePrevious = MathAngle.Angle360(pivotScreenPos,pointer.position);
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData pointer)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnPointerUp",this);
			angleTarget = 0;
			Refresh();
		}
		
		private float angleTarget = 0;
		private float anglePrevious = 0;
		private float angleCurrent = 0;
		void IDragHandler.OnDrag(PointerEventData pointer)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnDrag",this);
			angleCurrent = MathAngle.Angle360(pivotScreenPos,pointer.position);
			angleTarget += MathAngle.DeltaAngle(anglePrevious,angleCurrent);
			anglePrevious = angleCurrent;
			
			angleTarget = Mathf.Clamp(angleTarget,-rangeAngle,rangeAngle);
			Refresh();
		}
		#endregion
		
		#region Refresh
		private void Refresh()
		{
			axis.Value = MathFloat.Remap(1,-1,-rangeAngle,rangeAngle,angleTarget);
		}
		#endregion
	}
}
#endif
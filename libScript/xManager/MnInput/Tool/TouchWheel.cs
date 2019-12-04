#if xLibv2
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.Mathx;
using xLib.xInput;
using xLib.xNode.NodeObject;

namespace xLib.xTool.xInput
{
	public class TouchWheel : BaseM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public NodeFloat axis;
		public Transform pivot;
		public float rangeAngle = 480;
		
		#region Pointer
		private Vector2 pivotScreenPos;
		public void OnPointerDown(PointerEventData pointer)
		{
			angleTarget = -rangeAngle*axis.Value;
			Refresh();
			
			pivotScreenPos = pointer.pressEventCamera.WorldToScreenPoint(pivot.position);
			anglePrevious = MathAngle.Angle360(pivotScreenPos,pointer.position);
		}
		
		public void OnPointerUp(PointerEventData pointer)
		{
			angleTarget = 0;
			Refresh();
		}
		
		private float angleTarget;
		private float anglePrevious;
		private float angleCurrent;
		public void OnDrag(PointerEventData pointer)
		{
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
#if xLibv2
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xNode.NodeObject;

namespace xLib.xTool.xInput
{
	public class TouchJoystick : BaseM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		//[Separator("Try TouchRect")]
		public NodeFloat axisX;
		public NodeFloat axisY;
		public float range;
		private Vector2 vectorOutput;
		public Transform handle;
		
		#region Pointer
		void IPointerDownHandler.OnPointerDown(PointerEventData pointer)
		{
			Refresh(pointer);
		}
		
		void IDragHandler.OnDrag(PointerEventData pointer)
		{
			Refresh(pointer);
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData pointer)
		{
			handle.localPosition = Vector2.zero;
			
			vectorOutput = Vector2.zero;
			
			axisX.Value = vectorOutput.x;
			axisY.Value = vectorOutput.y;
		}
		#endregion
		
		#region Refresh
		private void Refresh(PointerEventData pointer)
		{
			handle.position = pointer.pressEventCamera.ScreenToWorldPoint(pointer.position);
			handle.localPosition = Vector2.ClampMagnitude(handle.localPosition, range);
			
			vectorOutput = handle.localPosition/range;
			
			axisX.Value = vectorOutput.x;
			axisY.Value = vectorOutput.y;
		}
		#endregion
	}
}
#endif
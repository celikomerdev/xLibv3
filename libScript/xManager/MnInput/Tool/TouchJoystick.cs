#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xValueClass;

namespace xLib.xTool.xInput
{
	public class TouchJoystick : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField]private NodeFloat axisX = null;
		[SerializeField]private NodeFloat axisY = null;
		[SerializeField]private float range = 100;
		[SerializeField]private Transform handle = null;
		private Vector2 vectorOutput = Vector2.zero;
		
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
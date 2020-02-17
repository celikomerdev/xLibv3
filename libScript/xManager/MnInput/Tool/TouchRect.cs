#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xValueClass;

namespace xLib.xTool.xInput
{
	public class TouchRect : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField]private NodeFloat axisX = null;
		[SerializeField]private NodeFloat axisY = null;
		[SerializeField]private Transform visual = null;
		[SerializeField]private Transform handle = null;
		[SerializeField]private bool canSnap = true;
		[SerializeField]private bool canMove = true;
		[SerializeField]private Vector2 range = Vector2.zero;
		
		#region Mono
		private void OnDisable()
		{
			if(canSnap || canMove) visual.localPosition = Vector2.zero;
			handle.localPosition = Vector2.zero;
			
			axisX.Value = 0;
			axisY.Value = 0;
		}
		#endregion
		
		#region Pointer
		void IPointerDownHandler.OnPointerDown(PointerEventData pointer)
		{
			if(canSnap) visual.position = pointer.pressEventCamera.ScreenToWorldPoint(pointer.position);
			Refresh(pointer);
		}
		
		void IDragHandler.OnDrag(PointerEventData pointer)
		{
			Refresh(pointer);
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData pointer)
		{
			OnDisable();
		}
		#endregion
		
		#region Refresh
		private void Refresh(PointerEventData pointer)
		{
			handle.position = pointer.pressEventCamera.ScreenToWorldPoint(pointer.position);
			Vector3 localPositionHandle = handle.localPosition;
			
			if(Mathx.MathVector3.IsClamp(ref localPositionHandle,-range,range))
			{
				if(canMove) visual.localPosition += (handle.localPosition - localPositionHandle);
				handle.localPosition = localPositionHandle;
			}
			
			axisX.Value = handle.localPosition.x/range.x;
			axisY.Value = handle.localPosition.y/range.y;
		}
		#endregion
	}
}
#endif
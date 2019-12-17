#if xLibv2
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xNode.NodeObject;

namespace xLib.xTool.xInput
{
	public class TouchRect : BaseM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public NodeFloat axisX;
		public NodeFloat axisY;
		public Transform visual;
		public Transform handle;
		public bool canSnap = true;
		public bool canMove = true;
		public Vector3 range;
		
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
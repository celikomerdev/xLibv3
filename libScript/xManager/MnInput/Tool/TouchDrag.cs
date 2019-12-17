#if xLibv2
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class TouchDrag : BaseM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public NodeFloat axisX;
		public NodeFloat axisY;
		public Vector2 multiplier;
		
		#region Pointer
		void IPointerDownHandler.OnPointerDown(PointerEventData pointer)
		{
			vectorPrev = pointer.position;
			Refresh(pointer);
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData pointer)
		{
			//vectorOutput = Vector2.zero;
			//axisX.Cache = vectorOutput.x;
			//axisY.Cache = vectorOutput.y;
		}
		
		void IDragHandler.OnDrag(PointerEventData pointer)
		{
			Refresh(pointer);
		}
		#endregion
		
		#region Refresh
		private Vector2 vectorDelta;
		private Vector2 vectorPrev;
		private Vector2 vectorOutput;
		private void Refresh(PointerEventData pointer)
		{
			vectorDelta = pointer.position - vectorPrev;
			vectorPrev = pointer.position;
			
			vectorOutput.x += vectorDelta.x*multiplier.x;
			vectorOutput.y += vectorDelta.y*multiplier.y;
			//vectorOutput = Math.MathVector2.Clamp(vectorOutput);
			
			axisX.Value = vectorOutput.x;
			axisY.Value = vectorOutput.y;
		}
		#endregion
	}
}
#endif
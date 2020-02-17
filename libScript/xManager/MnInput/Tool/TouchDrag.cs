#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xValueClass;

namespace xLib.xInput
{
	public class TouchDrag : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField]private NodeFloat axisX = null;
		[SerializeField]private NodeFloat axisY = null;
		[SerializeField]private Vector2 multiplier = Vector2.one;
		
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
		private Vector2 vectorDelta = Vector2.zero;
		private Vector2 vectorPrev = Vector2.zero;
		private Vector2 vectorOutput = Vector2.zero;
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
#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerScroll : BaseWorkM, IScrollHandler
	{
		public Vector2 multiplier = Vector2.one;
		public EventFloat pointerScrollX = new EventFloat();
		public EventFloat pointerScrollY = new EventFloat();
		
		void IScrollHandler.OnScroll(PointerEventData pointer)
		{
			pointerScrollX.Invoke(pointer.scrollDelta.x*multiplier.x);
			pointerScrollY.Invoke(pointer.scrollDelta.y*multiplier.y);
		}
	}
}
#endif
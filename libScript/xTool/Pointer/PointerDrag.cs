#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerDrag : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public int pointerCount = 1;
		private int pointerCountCurrent = 0;
		public EventFloat pointerDragX = new EventFloat();
		public EventFloat pointerDragY = new EventFloat();
		
		private void OnEnable()
		{
			pointerCountCurrent = 0;
		}
		
		private void OnDisable()
		{
			pointerCountCurrent = 0;
		}
		
		private void OnApplicationFocus(bool value)
		{
			if(!value) pointerCountCurrent = 0;
		}
		
		void IPointerDownHandler.OnPointerDown(PointerEventData pointer)
		{
			pointerCountCurrent++;
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData pointer)
		{
			if(pointerCountCurrent>0) pointerCountCurrent--;
		}
		
		void IDragHandler.OnDrag(PointerEventData pointer)
		{
			if(pointerCountCurrent != pointerCount) return;
			UpdatePointer(pointer);
		}
		
		private void UpdatePointer(PointerEventData pointer)
		{
			pointerDragX.Invoke(pointer.delta.x);
			pointerDragY.Invoke(pointer.delta.y);
		}
	}
}
#endif
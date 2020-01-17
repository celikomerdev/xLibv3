#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerHold : BaseTickNodeM, IPointerDownHandler, IPointerUpHandler
	{
		private bool value = false;
		public EventUnity pointerHold = new EventUnity();
		
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			value = true;
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			value = false;
		}
		
		protected override void Tick(float tickTime)
		{
			if(!value) return;
			pointerHold.Invoke();
		}
	}
}
#endif
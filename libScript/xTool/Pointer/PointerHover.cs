#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerHover : BaseTickNodeM, IPointerEnterHandler, IPointerExitHandler
	{
		private bool value = false;
		public EventUnity pointerHover = new EventUnity();
		
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			value = true;
		}
		
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			value = false;
		}
		
		protected override void Tick(float tickTime)
		{
			if(!value) return;
			pointerHover.Invoke();
		}
	}
}
#endif
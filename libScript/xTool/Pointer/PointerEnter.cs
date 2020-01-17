#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerEnter : BaseWorkM, IPointerEnterHandler, IPointerExitHandler
	{
		public EventBool pointerEnter = new EventBool();
		
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			pointerEnter.Invoke(true);
		}
		
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			pointerEnter.Invoke(false);
		}
	}
}
#endif
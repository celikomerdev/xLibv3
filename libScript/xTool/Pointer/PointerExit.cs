#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerExit : BaseWorkM, IPointerEnterHandler, IPointerExitHandler
	{
		public EventBool pointerExit = new EventBool();
		
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if(!CanWork) return;
			pointerExit.Invoke(false);
		}
		
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if(!CanWork) return;
			pointerExit.Invoke(true);
		}
	}
}
#endif
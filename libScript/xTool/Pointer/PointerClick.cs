#if xLibv3
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerClick : BaseWorkM, IPointerClickHandler
	{
		public EventUnity pointerClick = new EventUnity();
		
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if(!CanWork) return;
			pointerClick.Invoke();
		}
	}
}
#endif
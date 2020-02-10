#if xLibv3
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerDown : BaseWorkM, IPointerDownHandler, IPointerUpHandler
	{
		public bool cumulative = true;
		private int pointerCountCurrent = 0;
		public int PointerCount
		{
			get
			{
				return pointerCountCurrent;
			}
			set
			{
				if(!CanWork) return;
				if(CanDebug) Debug.Log($"{this.name}:pointerCountCurrent:{value}",this);
				if(value<0) return;
				if(value==pointerCountCurrent) return;
				
				if(cumulative)
				{
					switch (value)
					{
						case 1:
							CallPointerDown(true);
							break;
						case 0:
							CallPointerDown(false);
							break;
					}
				}
				else
				{
					CallPointerDown(value>pointerCountCurrent);
				}
				
				pointerCountCurrent = value;
			}
		}
		
		public EventBool pointerDown = new EventBool();
		public void CallPointerDown(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:CallPointerDown:{value}",this);
			pointerDown.Invoke(value);
		}
		
		private void OnEnable()
		{
			PointerCount = 0;
		}
		
		private void OnDisable()
		{
			PointerCount = 0;
		}
		
		private void OnApplicationFocus(bool value)
		{
			if(value) return;
			PointerCount = 0;
		}
		
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			PointerCount++;
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			PointerCount--;
		}
	}
}
#endif
#if xLibv3
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.EventClass;

namespace xLib.ToolPointer
{
	public class PointerPinch : BaseWorkM, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		private Dictionary<int,PointerData> dictionary = new Dictionary<int, PointerData>();
		public EventFloat pointerPinch = new EventFloat();
		
		void IPointerDownHandler.OnPointerDown(PointerEventData value)
		{
			lastDistance = 0;
			PointerData pointerDataNew = new PointerData();
			pointerDataNew.pointer = value;
			dictionary.Add(value.pointerId,pointerDataNew);
		}
		
		void IPointerUpHandler.OnPointerUp(PointerEventData value)
		{
			lastDistance = 0;
			dictionary.Remove(value.pointerId);
		}
		
		void IDragHandler.OnDrag(PointerEventData value)
		{
			dictionary[value.pointerId].SetPosition(value.position);
			Work();
		}
		
		private float lastDistance = 0;
		private float currentDistance = 0;
		private float deltaDistance = 0;
		private void Work()
		{
			if(dictionary.Count!=2) return;
			
			currentDistance = (dictionary.ElementAt(0).Value.pointer.position - dictionary.ElementAt(1).Value.pointer.position).magnitude;
			if(lastDistance==0) lastDistance = currentDistance;
			
			deltaDistance = currentDistance - lastDistance;
			lastDistance = currentDistance;
			
			pointerPinch.Invoke(deltaDistance);
		}
	}
}

namespace xLib
{
	public class PointerData
	{
		public PointerEventData pointer = null;
		private float time = 0;
		public Vector2 lastPosition = Vector2.zero;
		
		public void SetPosition(Vector2 value)
		{
			time = Time.realtimeSinceStartup;
			if(lastPosition==Vector2.zero) lastPosition = value;
			
			deltaPosition = lastPosition - value;
			lastPosition = value;
		}
		
		private Vector2 deltaPosition = Vector2.zero;
		public Vector2 DeltaPosition
		{
			get
			{
				if(time != Time.realtimeSinceStartup) deltaPosition = Vector2.zero;
				return deltaPosition;
			}
		}
	}
}
#endif










// namespace xLib.OnEvent
// {
// 	public class OnPointerPinch_ : BaseM, IPinchHandler
// 	{
// 		public OnStringFloat axis;
		
// 		void IPinchHandler.OnPinch(SimpleGestures sender, MultiTouchPointerEventData eventData, Vector2 pinchDelta)
// 		{
// 			axis.Value = pinchDelta.x + pinchDelta.y;
// 		}
// 	}
// }
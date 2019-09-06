#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventBool
	{
		[SerializeField]public EventBaseBool eventBool = new EventBaseBool();
		[SerializeField]public EventBaseBool eventReverse = new EventBaseBool();
		[SerializeField]public EventBaseUnity eventTrue = new EventBaseUnity();
		[SerializeField]public EventBaseUnity eventFalse = new EventBaseUnity();
		
		public void Invoke(bool arg0)
		{
			eventBool.Invoke(arg0);
			eventReverse.Invoke(!arg0);
			
			if(arg0) eventTrue.Invoke();
			else eventFalse.Invoke();
		}
		
		public bool Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
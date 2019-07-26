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
		[SerializeField]private EventBaseBool eventReverse = new EventBaseBool();
		[SerializeField]private EventBaseUnity eventTrue = new EventBaseUnity();
		[SerializeField]private EventBaseUnity eventFalse = new EventBaseUnity();
		
		public void Invoke(bool arg0)
		{
			eventBool.Invoke(arg0);
			eventReverse.Invoke(!arg0);
			
			if(arg0) eventTrue.Invoke();
			else eventFalse.Invoke();
		}
	}
}
#endif
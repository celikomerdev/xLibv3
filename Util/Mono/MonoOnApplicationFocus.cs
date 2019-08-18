﻿#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnApplicationFocus : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onApplicationFocus")]
		[SerializeField]private EventBool eventApplicationFocus = new EventBool();
		
		private void OnApplicationFocus(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":MonoOnApplicationFocus:{0}",value);
			eventApplicationFocus.Invoke(value);
		}
	}
}
#endif
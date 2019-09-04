﻿#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventVector3
	{
		[SerializeField]public EventBaseVector3 eventVector3 = new EventBaseVector3();
		
		public void Invoke(Vector3 arg0)
		{
			eventVector3.Invoke(arg0);
		}
		
		public Vector3 Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
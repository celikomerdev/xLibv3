﻿#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventByte
	{
		[SerializeField]public EventBaseByte eventByte = new EventBaseByte();
		
		public void Invoke(byte arg0)
		{
			eventByte.Invoke(arg0);
		}
		
		public byte Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
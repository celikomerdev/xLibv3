﻿#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class CallColor : BaseMainM
	{
		[SerializeField]private Color value = Color.white;
		public EventColor eventColor;
		
		public void Call()
		{
			eventColor.Invoke(value);
		}
	}
}
#endif
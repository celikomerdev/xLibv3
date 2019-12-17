﻿#if xLibv2
#if GooglePlayService
using xLib.ToolEventClass;

namespace xLib.UnityEvents
{
	public class OnMatchLeave : BaseRegisterM
	{
		protected override bool Register(bool value)
		{
			if (value) MnMatchTurn.ins.onMatchLeave += OnCall;
			else MnMatchTurn.ins.onMatchLeave -= OnCall;
			return value;
		}
		
		public uint variant = 0;
		public EventUnity eventUnity;
		private void OnCall()
		{
			if (variant == 0 || variant == MnMatchTurn.ins.variant) eventUnity.Invoke();
		}
	}
}
#else
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.UnityEvents
{
	public class OnMatchLeave : BaseRegisterM
	{
		public uint variant = 0;
		public EventUnity eventUnity;
	}
}
#endif
#endif
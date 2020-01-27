#if xLibv2
#if GooglePlayService
using xLib.ToolEventClass;

namespace xLib.UnityEvents
{
	public class OnTurnYour : BaseRegisterM
	{
		protected override bool TryRegister(bool value)
		{
			if (value) MnMatchTurn.ins.onTurnYour += OnCall;
			else MnMatchTurn.ins.onTurnYour -= OnCall;
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
	public class OnTurnYour : BaseRegisterM
	{
		public uint variant = 0;
		public EventUnity eventUnity;
	}
}
#endif
#endif
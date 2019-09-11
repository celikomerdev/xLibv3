﻿#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public class ActionSortedSingle<T0> : ActionSortedBase<T0>
	{
		private ActionSorted<T0> actionSortedSingle = new ActionSorted<T0>();
		
		public override void Listener(bool register,UnityAction<T0> call)
		{
			actionSortedSingle.Listener(register,call);
		}
		
		public override void Invoke(T0 arg0)
		{
			actionSortedSingle.Invoke(arg0);
		}
		
		public override void InvokeFirst(T0 arg0)
		{
			actionSortedSingle.InvokeFirst(arg0);
		}
		
		public override void InvokeLast(T0 arg0)
		{
			actionSortedSingle.InvokeLast(arg0);
		}
	}
}
#endif
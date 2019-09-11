#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public abstract class ActionSortedBase<T0>
	{
		public abstract void Listener(bool register,UnityAction<T0> call);
		public abstract void Invoke(T0 arg0);
		public abstract void InvokeFirst(T0 arg0);
		public abstract void InvokeLast(T0 arg0);
	}
}
#endif
#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public abstract class ActionSortedBase<T0>
	{
		public abstract void Listener(bool register,UnityAction<T0> call,string viewId,int order);
		public abstract void Invoke(T0 arg0,string viewId);
		public abstract void InvokeFirst(T0 arg0,string viewId);
		public abstract void InvokeLast(T0 arg0,string viewId);
	}
}
#endif
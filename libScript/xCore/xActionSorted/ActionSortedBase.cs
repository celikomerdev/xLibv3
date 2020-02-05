#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public abstract class ActionSortedBase<V>
	{
		public abstract void Listener(bool register,UnityAction<V> call,string viewId,int order);
		public abstract void Invoke(V value,string viewId);
		public abstract void InvokeFirst(V value,string viewId);
		public abstract void InvokeLast(V value,string viewId);
	}
}
#endif
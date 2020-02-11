#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public class ActionSortedSingle<V> : ActionSortedBase<V>
	{
		private readonly ActionSorted<V> actionSortedSingle = new ActionSorted<V>();
		
		public override void Listener(bool register,UnityAction<V> call,string viewId,int order)
		{
			actionSortedSingle.Listener(register,call,order);
		}
		
		public override void Invoke(V value,string viewId)
		{
			actionSortedSingle.Invoke(value);
		}
		
		public override void InvokeFirst(V value,string viewId)
		{
			actionSortedSingle.InvokeFirst(value);
		}
		
		public override void InvokeLast(V value,string viewId)
		{
			actionSortedSingle.InvokeLast(value);
		}
	}
}
#endif
#if xLibv3
using System.Collections.Generic;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSortedMulti<V> : ActionSortedBase<V>
	{
		private readonly Dictionary<string,ActionSorted<V>> actionSortedMulti = new Dictionary<string,ActionSorted<V>>();
		
		public override void Listener(bool register,UnityAction<V> call,string viewId,int order)
		{
			if(register) CreateId(viewId);
			actionSortedMulti[viewId].Listener(register,call,order);
		}
		
		public override void Invoke(V value,string viewId)
		{
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].Invoke(value);
		}
		
		public override void InvokeFirst(V value,string viewId)
		{
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].InvokeFirst(value);
		}
		
		public override void InvokeLast(V value,string viewId)
		{
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].InvokeLast(value);
		}
		
		private void CreateId(string viewId)
		{
			if(actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti.Add(viewId,new ActionSorted<V>());
		}
	}
}
#endif
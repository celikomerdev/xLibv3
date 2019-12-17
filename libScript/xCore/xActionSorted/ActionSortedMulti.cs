#if xLibv3
using System.Collections.Generic;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSortedMulti<T0>:ActionSortedBase<T0>
	{
		private Dictionary<string,ActionSorted<T0>> actionSortedMulti = new Dictionary<string,ActionSorted<T0>>();
		
		public override void Listener(bool register,UnityAction<T0> call,string viewId,int order)
		{
			ViewCore.FinalizeId();
			if(register) CreateId(viewId);
			actionSortedMulti[viewId].Listener(register,call,order);
		}
		
		public override void Invoke(T0 arg0,string viewId)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].Invoke(arg0);
		}
		
		public override void InvokeFirst(T0 arg0,string viewId)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].InvokeFirst(arg0);
		}
		
		public override void InvokeLast(T0 arg0,string viewId)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti[viewId].InvokeLast(arg0);
		}
		
		private void CreateId(string viewId)
		{
			if(actionSortedMulti.ContainsKey(viewId)) return;
			actionSortedMulti.Add(viewId,new ActionSorted<T0>());
		}
	}
}
#endif
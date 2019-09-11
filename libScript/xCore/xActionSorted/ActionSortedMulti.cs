#if xLibv3
using System.Collections.Generic;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSortedMulti<T0> : ActionSortedBase<T0>
	{
		private Dictionary<string,ActionSorted<T0>> actionSortedMulti = new Dictionary<string,ActionSorted<T0>>();
		
		public override void Listener(bool register,UnityAction<T0> call)
		{
			ViewCore.FinalizeId();
			if(register) CreateId();
			actionSortedMulti[ViewCore.FinalId].Listener(register,call);
		}
		
		public override void Invoke(T0 arg0)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(ViewCore.FinalId)) return;
			actionSortedMulti[ViewCore.FinalId].Invoke(arg0);
		}
		
		public override void InvokeFirst(T0 arg0)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(ViewCore.FinalId)) return;
			actionSortedMulti[ViewCore.FinalId].InvokeFirst(arg0);
		}
		
		public override void InvokeLast(T0 arg0)
		{
			ViewCore.FinalizeId();
			if(!actionSortedMulti.ContainsKey(ViewCore.FinalId)) return;
			actionSortedMulti[ViewCore.FinalId].InvokeLast(arg0);
		}
		
		private void CreateId()
		{
			if(actionSortedMulti.ContainsKey(ViewCore.FinalId)) return;
			actionSortedMulti.Add(ViewCore.FinalId,new ActionSorted<T0>());
		}
	}
}
#endif
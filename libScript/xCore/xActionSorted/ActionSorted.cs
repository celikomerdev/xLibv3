#if xLibv3
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSorted<T0>
	{
		private SortedList<int,UnityAction<T0>> sortedList = new SortedList<int,UnityAction<T0>>();
		
		public void Listener(bool register,UnityAction<T0> call,int order)
		{
			if(register)
			{
				CreateOrder(order);
				sortedList[order] += call;
			}
			else
			{
				if(!sortedList.ContainsKey(order)) return;
				sortedList[order] -= call;
				ClearOrder(order);
			}
		}
		
		public void Invoke(T0 arg0)
		{
			for (int i = 0; i < sortedList.Count; i++)
			{
				sortedList.ElementAt(i).Value.Invoke(arg0);
			}
		}
		
		public void InvokeFirst(T0 arg0)
		{
			if(sortedList.Count==0) return;
			UnityAction<T0> temp = (UnityAction<T0>)sortedList.FirstOrDefault().Value.GetInvocationList().FirstOrDefault();
			temp.Invoke(arg0);
		}
		
		public void InvokeLast(T0 arg0)
		{
			if(sortedList.Count==0) return;
			UnityAction<T0> temp = (UnityAction<T0>)sortedList.LastOrDefault().Value.GetInvocationList().LastOrDefault();
			temp.Invoke(arg0);
		}
		
		private void CreateOrder(int order)
		{
			if(sortedList.ContainsKey(order)) return;
			sortedList.Add(order,null);
		}
		
		private void ClearOrder(int order)
		{
			if(!sortedList.ContainsKey(order)) return;
			if(sortedList[order]!=null) return;
			sortedList.Remove(order);
		}
	}
}
#endif
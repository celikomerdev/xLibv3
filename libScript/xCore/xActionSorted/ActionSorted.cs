#if xLibv3
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSorted<V>
	{
		private SortedList<int,UnityAction<V>> sortedList = new SortedList<int,UnityAction<V>>();
		
		public void Listener(bool register,UnityAction<V> call,int order)
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
		
		public void Invoke(V value)
		{
			for (int i = 0; i < sortedList.Count; i++)
			{
				sortedList.ElementAt(i).Value.Invoke(value);
			}
		}
		
		public void InvokeFirst(V value)
		{
			if(sortedList.Count==0) return;
			UnityAction<V> temp = (UnityAction<V>)sortedList.FirstOrDefault().Value.GetInvocationList().FirstOrDefault();
			temp.Invoke(value);
		}
		
		public void InvokeLast(V value)
		{
			if(sortedList.Count==0) return;
			UnityAction<V> temp = (UnityAction<V>)sortedList.LastOrDefault().Value.GetInvocationList().LastOrDefault();
			temp.Invoke(value);
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
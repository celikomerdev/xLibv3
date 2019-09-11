#if xLibv3
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace xLib
{
	public class ActionSorted<T0>
	{
		private SortedList<int,UnityAction<T0>> sortedList = new SortedList<int,UnityAction<T0>>();
		
		public void Listener(bool register,UnityAction<T0> call)
		{
			if(register)
			{
				CreateOrder();
				sortedList[BaseRegisterM.Order] += call;
			}
			else
			{
				if(!sortedList.ContainsKey(BaseRegisterM.Order)) return;
				sortedList[BaseRegisterM.Order] -= call;
				ClearOrder();
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
		
		private void CreateOrder()
		{
			if(sortedList.ContainsKey(BaseRegisterM.Order)) return;
			
			// #if CanDebug
			// xDebug.LogTempFormat(BaseRegisterM.OrderObject,"sortedList.Add{0}",BaseRegisterM.Order);
			// #endif
			sortedList.Add(BaseRegisterM.Order,null);
		}
		
		private void ClearOrder()
		{
			if(!sortedList.ContainsKey(BaseRegisterM.Order)) return;
			if(sortedList[BaseRegisterM.Order]!=null) return;
			
			// #if CanDebug
			// xDebug.LogTempFormat(BaseRegisterM.OrderObject,"sortedList.Remove{0}",BaseRegisterM.Order);
			// #endif
			sortedList.Remove(BaseRegisterM.Order);
		}
	}
}
#endif
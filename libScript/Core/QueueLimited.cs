#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public struct QueueLimited<T>
	{
		public Queue<T> queue;
		public int limit;
		
		public QueueLimited(int limit = int.MaxValue)
		{
			queue = new Queue<T>();
			this.limit = limit;
		}
		
		public void Enqueue(T obj)
		{
			queue.Enqueue(obj);
			
			while(queue.Count>limit)
			{
				queue.Dequeue();
			}
		}
	}
}
#endif
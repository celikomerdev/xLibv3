#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public class CoroutineQueue
	{
		private readonly MonoBehaviour owner = null;
		private Coroutine internalCoroutine = null;
		private readonly Queue<IEnumerator> actions = new Queue<IEnumerator>();
		
		public CoroutineQueue(MonoBehaviour coroutineOwner)
		{
			owner = coroutineOwner;
		}
		
		public void StartLoop()
		{
			internalCoroutine = owner.NewCoroutine(Process());
		}
		
		public void StopLoop()
		{
			owner.StopCoroutine(internalCoroutine);
			internalCoroutine = null;
		}
		
		public void EnqueueAction(IEnumerator valueAction)
		{
			actions.Enqueue(valueAction);
		}
		
		private IEnumerator Process()
		{
			while(true)
			{
				if (actions.Count > 0)
				{
					yield return owner.NewCoroutine(actions.Dequeue());
				}
				else
				{
					yield return new WaitForEndOfFrame();
				}
			}
		}
	}
}
#endif
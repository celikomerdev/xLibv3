#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnPopupBar : SingletonM<MnPopupBar>
	{
		[SerializeField]private EventString eventString = null;
		
		#region Queue
		private QueueLimited<string> queue = new QueueLimited<string>();
		[SerializeField]private int queueLimit = 5;
		#endregion
		
		#region Mono
		protected override void Awaked()
		{
			queue.limit = queueLimit;
		}
		#endregion
		
		#region Show
		private bool isBusy = false;
		public bool IsBusy
		{
			set
			{
				if(isBusy == value) return;
				isBusy = value;
				TryShowQueue();
			}
		}
		
		public void QueueMessage(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":QueueMessage:{0}",value);
			queue.Enqueue(value);
			TryShowQueue();
		}
		
		private void TryShowQueue()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":TryShowQueue");
			if (queue.queue.Count == 0) return;
			if(isBusy) return;
			isBusy = true;
			
			string message = queue.queue.Dequeue();
			eventString.Invoke(message);
		}
		#endregion
	}
}
#endif
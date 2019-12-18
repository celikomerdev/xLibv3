#if xLibv3
using System.Threading;
using UnityEngine.Events;
using xLib.EventClass;

namespace xLib
{
	public class MnThread : SingletonM<MnThread>
	{
		#region Mono
		private void Update()
		{
			Call();
		}
		
		private static void Call()
		{
			if(!willCall) return;
			willCall = false;
			
			EventUnity cacheListener = listener;
			listener = new EventUnity();
			
			cacheListener.Invoke();
			cacheListener.eventUnity.RemoveAllListeners();
		}
		#endregion
		
		#region Flow
		private static bool willCall;
		private static EventUnity listener = new EventUnity();
		public static void Register(UnityAction call)
		{
			listener.eventUnity.AddListener(call);
			willCall = true;
		}
		
		public static void StartThread(UnityAction call)
		{
			Thread thread = new Thread(new ThreadStart(call));
			thread.Start();
		}
		#endregion
	}
}
#endif
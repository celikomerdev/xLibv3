#if xLibv3
using System.Threading;
using UnityEngine.Events;

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
			
			UnityEvent tempListener = listener;
			listener = new UnityEvent();
			
			tempListener.Invoke();
			tempListener.RemoveAllListeners();
		}
		#endregion
		
		#region Flow
		private static bool willCall;
		private static UnityEvent listener = new UnityEvent();
		public static void Register(UnityAction call)
		{
			listener.AddListener(call);
			willCall = true;
		}
		
		public static void StartThread(UnityAction call)
		{
			Thread t = new Thread(new ThreadStart(call));
			t.Start();
		}
		#endregion
	}
}
#endif
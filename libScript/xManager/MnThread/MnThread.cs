#if xLibv3
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using xLib.EventClass;

namespace xLib
{
	public class MnThread : SingletonM<MnThread>
	{
		#region Mono
		private void LateUpdate()
		{
			if(CanWork) Call();
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
		private static bool willCall = false;
		private static EventUnity listener = new EventUnity();
		public static void Register(UnityAction call,IDebug iDebug=null)
		{
			if(iDebug.CanDebug) Debug.Log($"MnThread:Register:{call.Method}",iDebug.UnityObject);
			listener.eventUnity.AddListener(call);
			willCall = true;
		}
		
		public static void StartThread(UnityAction call,bool useThread=true,int priority = 2,IDebug iDebug=null)
		{
			if(iDebug!=null && iDebug.CanDebug) Debug.Log($"MnThread:StartThread:{useThread}:{call.Method}",iDebug.UnityObject);
			if(!useThread)
			{
				call();
				return;
			}
			Thread thread = new Thread(new ThreadStart(call));
			thread.Priority = (System.Threading.ThreadPriority)priority;
			thread.Start();
		}
		#endregion
	}
}
#endif
#if xLibv3
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using xLib.EventClass;

namespace xLib
{
	public class MnThread : SingletonM<MnThread>
	{
		[SerializeField]private bool forceSingle = false;
		
		#region Mono
		private void LateUpdate()
		{
			if(CanWork) Call();
		}
		
		private static void Call()
		{
			if(!willCall) return;
			willCall = false;
			if(ins.CanDebug) Debug.Log($"{ins.name}:Call",ins);
			
			EventUnity cacheListener = listener;
			listener = new EventUnity();
			
			cacheListener.Invoke();
			cacheListener.eventUnity.RemoveAllListeners();
		}
		#endregion
		
		#region Flow
		private static bool willCall = false;
		private static EventUnity listener = new EventUnity();
		public static void ScheduleLate(UnityAction call,IDebug iDebug=null)
		{
			// if(iDebug.CanDebug) Debug.Log($"MnThread:ScheduleLate:{call.Method}",iDebug.UnityObject);
			listener.eventUnity.AddListener(call);
			willCall = true;
		}
		
		public static void StartThread(UnityAction call,bool useThread=true,int priority = 2,IDebug iDebug=null)
		{
			if(ins.forceSingle) useThread = false;
			if(ins.CanDebug && useThread)
			{
				if(iDebug!=null) Debug.Log($"MnThread:StartThread:{useThread}:{call.Method}",iDebug.UnityObject);
				else Debug.Log($"MnThread:StartThread:{useThread}:{call.Method}");
			}
			
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
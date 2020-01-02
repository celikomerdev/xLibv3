#if xLibv3
using UnityEngine;

namespace xLib
{
	public class xLogger : BaseMainM
	{
		#region Debug
		private static bool canDebug = true;
		public static bool CanDebug
		{
			get
			{
				#if CanDebug
				return canDebug;
				#else
				return false;
				#endif
			}
			set
			{
				canDebug = value;
			}
		}
		public bool SetDebug
		{
			set
			{
				xLogger.CanDebug = value;
			}
		}
		
		private static bool canTest = false;
		public static bool CanTest
		{
			get
			{
				#if CanDebug
				return canTest;
				#else
				return false;
				#endif
			}
			set
			{
				canDebug = value;
			}
		}
		public bool SetTest
		{
			set
			{
				xLogger.CanTest = value;
			}
		}
		#endregion
		
		
		#region Exception
		public static void Log(object message)
		{
			if(!CanDebug) return;
			Debug.LogFormat(message.ToString());
		}
		
		public static void Log(object message, Object context)
		{
			if(!CanDebug) return;
			Debug.LogFormat(context,message.ToString());
		}
		
		public static void LogFormat(string format, params object[] args)
		{
			if(!CanDebug) return;
			Debug.LogFormat(format,args);
		}
		
		public static void LogFormat(Object context, string format, params object[] args)
		{
			if(!CanDebug) return;
			Debug.LogFormat(context,format,args);
		}
		#endregion
		
		
		#region Exception
		public static void LogException(object message)
		{
			Debug.LogException(new UnityException(message.ToString()));
		}
		
		public static void LogException(object message, Object context)
		{
			Debug.LogException(new UnityException(message.ToString()),context);
		}
		
		public static void LogExceptionFormat(string format, params object[] args)
		{
			string message = string.Format(format,args);
			Debug.LogException(new UnityException(message));
		}
		
		public static void LogExceptionFormat(Object context, string format, params object[] args)
		{
			string message = string.Format(format,args);
			Debug.LogException(new UnityException(message),context);
		}
		#endregion
	}
}
#endif
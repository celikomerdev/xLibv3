#if xLibv3
using UnityEngine;

namespace xLib
{
	public class xDebug : BaseMainM
	{
		#region Debug
		public static bool CanDebug = true;
		public bool SetDebug
		{
			set
			{
				xDebug.CanDebug = value;
			}
		}
		
		public static bool CanTest = false;
		public bool SetTest
		{
			set
			{
				xDebug.CanTest = value;
			}
		}
		#endregion
		
		
		#region Exception
		public static void LogTemp(object message)
		{
			if(!CanDebug) return;
			Debug.LogFormat(message.ToString());
		}
		
		public static void LogTemp(object message, Object context)
		{
			if(!CanDebug) return;
			Debug.LogFormat(context,message.ToString());
		}
		
		public static void LogTempFormat(string format, params object[] args)
		{
			if(!CanDebug) return;
			Debug.LogFormat(format,args);
		}
		
		public static void LogTempFormat(Object context, string format, params object[] args)
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
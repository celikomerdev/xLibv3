#if xLibv3
using UnityEngine;

namespace xLib
{
	[System.Serializable]internal class DebugLevel
	{
		[SerializeField]private bool error = false;
		[SerializeField]private bool assert = false;
		[SerializeField]private bool warning = false;
		[SerializeField]private bool log = false;
		[SerializeField]private bool exception = false;
		[SerializeField]internal bool trace = false;
		
		
		internal bool CanLog(LogType type)
		{
			switch (type)
			{
				case LogType.Error:
					return error;
				case LogType.Assert:
					return assert;
				case LogType.Warning:
					return warning;
				case LogType.Log:
					return log;
				case LogType.Exception:
					return exception;
				default:
					return true;
			}
		}
	}
}
#endif
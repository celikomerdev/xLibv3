#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnDebug : BaseMainM
	{
		[SerializeField]private int length = 1000;
		[SerializeField]private DebugLevel debugLevel = new DebugLevel();
		[SerializeField]private EventString eventString = null;
		
		#region Mono
		private void OnEnable()
		{
			Application.logMessageReceivedThreaded += HandleLog;
		}
		
		private void OnDisable()
		{
			Application.logMessageReceivedThreaded -= HandleLog;
			ClearLog();
		}
		#endregion
		
		
		#region HandleLog
		private void HandleLog(string logString, string stackTrace, LogType type)
		{
			if (!debugLevel.CanLog(type)) return;
			
			AddLog(logString);
			if(debugLevel.trace) AddLog(stackTrace);
		}
		
		private bool isClean = true;
		private string stringDebug = "";
		private void AddLog(string value)
		{
			stringDebug += "\n"+value;
			while(stringDebug.Length > length*2)
			{
				stringDebug = stringDebug.Remove(0,length);
			}
			isClean = false;
		}
		
		public void ClearLog()
		{
			stringDebug = "";
			isClean = false;
			eventString.Invoke(stringDebug);
		}
		#endregion
		
		
		#region Mono
		private void LateUpdate()
		{
			if(isClean) return;
			isClean = true;
			eventString.Invoke(stringDebug);
		}
		#endregion
	}
}
#endif
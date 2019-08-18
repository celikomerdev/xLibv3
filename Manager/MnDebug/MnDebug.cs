﻿#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnDebug : BaseRegisterM
	{
		[Header("referance")]
		[SerializeField]private int length = 1000;
		[SerializeField]private EventString eventString = null;
		
		[SerializeField]private DebugLevel debugLevel = new DebugLevel();
		
		private bool isClean = true;
		private string stringDebug = "";
		
		
		#region Register
		protected override bool OnRegister(bool value)
		{
			if (value) Application.logMessageReceived += HandleLog;
			else Application.logMessageReceived -= HandleLog;
			return value;
		}
		#endregion
		
		
		#region HandleLog
		private void HandleLog(string logString, string stackTrace, LogType type)
		{
			if (!debugLevel.CanLog(type)) return;
			
			AddLog(logString);
			if(debugLevel.trace) AddLog(stackTrace);
		}
		
		private void AddLog(string value)
		{
			stringDebug += "\n"+value;
			if(stringDebug.Length > length*2) stringDebug = stringDebug.Remove(0,length);
			isClean = false;
		}
		
		public void ClearLog()
		{
			stringDebug = "";
			isClean = false;
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
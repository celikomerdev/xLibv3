#if xLibv3
using UnityEngine;

namespace xLib.ToolDebug
{
	public class LogWarning : BaseWorkM
	{
		public void LogBool(bool value)
		{
			if(CanDebug) Debug.LogWarning(value,this);
		}
		
		public void LogFloat(float value)
		{
			if(CanDebug) Debug.LogWarning(value,this);
		}
		
		public void LogInt(int value)
		{
			if(CanDebug) Debug.LogWarning(value,this);
		}
		
		public void LogString(string value)
		{
			if(CanDebug) Debug.LogWarning(value,this);
		}
	}
}
#endif
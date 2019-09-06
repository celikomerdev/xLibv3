#if xLibv3
using UnityEngine;

namespace xLib.ToolDebug
{
	public class LogError : BaseWorkM
	{
		public void LogBool(bool value)
		{
			if(CanDebug) Debug.LogErrorFormat(this,this.name+":LogBool:{0}",value);
		}
		
		public void LogFloat(float value)
		{
			if(CanDebug) Debug.LogErrorFormat(this,this.name+":LogFloat:{0}",value);
		}
		
		public void LogInt(int value)
		{
			if(CanDebug) Debug.LogErrorFormat(this,this.name+":LogInt:{0}",value);
		}
		
		public void LogString(string value)
		{
			if(CanDebug) Debug.LogErrorFormat(this,this.name+":LogString:{0}",value);
		}
	}
}
#endif
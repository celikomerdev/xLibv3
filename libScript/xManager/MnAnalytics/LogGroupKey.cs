#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib.ToolManager
{
	public class LogGroupKey : BaseWorkM
	{
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private IAnalyticObject[] m_arrayIAnalyticObject = new IAnalyticObject[0];
		
		private void Start()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Start");
			m_arrayIAnalyticObject = m_arrayIAnalyticObject.GetGenericsArray<IAnalyticObject>();
		}
		
		private void OnCall()
		{
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				IAnalyticObject analyticObject = m_arrayIAnalyticObject[i];
				if(!analyticObject.AnalyticDirty) continue;
				analyticObject.AnalyticDirty = false;
				if(CanDebug) Debug.LogFormat(analyticObject.UnityObject,"AnalyticsSend:{0}:{1}:{2}:{3}",ViewCore.CurrentId,analyticObject.Name,analyticObject.AnalyticString,analyticObject.AnalyticDigit);
				Send(analyticObject);
			}
		}
		
		protected virtual void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent(key:"object",label:analyticObject.Key,digit:analyticObject.AnalyticDigit,data:new Dictionary<string,object>{{"value",analyticObject.AnalyticString}});
		}
	}
}
#endif
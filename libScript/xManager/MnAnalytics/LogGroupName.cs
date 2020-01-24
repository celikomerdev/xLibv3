#if xLibv3
using System.Collections.Generic;

namespace xLib.ToolManager
{
	public class LogGroupName : LogGroup
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent(group:"node",key:analyticObject.Name,digit:analyticObject.AnalyticDigit,data:analyticObject.AnalyticString,dict:new Dictionary<string,object>{{"value",analyticObject.AnalyticObject}});
		}
	}
}
#endif
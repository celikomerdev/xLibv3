#if xLibv3
using System.Collections.Generic;

namespace xLib.ToolManager
{
	public class LogGroupName : LogGroup
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent(key:"object",label:analyticObject.Name,digit:analyticObject.AnalyticDigit,data:new Dictionary<string,object>{{"value",analyticObject.AnalyticString}});
		}
	}
}
#endif
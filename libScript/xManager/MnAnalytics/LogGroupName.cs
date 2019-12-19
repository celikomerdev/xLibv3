#if xLibv3
using System.Collections.Generic;

namespace xLib.ToolManager
{
	public class LogGroupName : LogGroupKey
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent(key:"object",digit:analyticObject.AnalyticDigit,data:new Dictionary<string,object>{{"name",analyticObject.Name},{"value",analyticObject.AnalyticString}});
		}
	}
}
#endif
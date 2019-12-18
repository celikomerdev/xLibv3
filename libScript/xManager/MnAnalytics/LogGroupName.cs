#if xLibv3
namespace xLib.ToolManager
{
	public class LogGroupName : LogGroupKey
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent("Value",analyticObject.Name,analyticObject.AnalyticString,analyticObject.AnalyticDigit);
		}
	}
}
#endif
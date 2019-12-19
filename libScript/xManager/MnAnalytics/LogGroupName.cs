#if xLibv3
namespace xLib.ToolManager
{
	public class LogGroupName : LogGroupKey
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			StAnalytics.LogEvent(key:"Value", label:analyticObject.Name, data:analyticObject.AnalyticString, digit:analyticObject.AnalyticDigit);
		}
	}
}
#endif
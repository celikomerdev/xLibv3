#if xLibv3
namespace xLib.ToolManager
{
	public class LogGroupName : LogGroupKey
	{
		protected override void Send(IAnalyticObject analyticObject)
		{
			MnAnalytics.ins.LogEvent("Value",analyticObject.Name,analyticObject.AnalyticString,analyticObject.AnalyticDigit);
		}
	}
}
#endif
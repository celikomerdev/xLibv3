#if xLibv3
namespace xLib
{
	public interface IAnalyticObject
	{
		UnityEngine.Object UnityObject
		{
			get;
		}
		
		string Key
		{
			get;
		}
		
		string Name
		{
			get;
		}
		
		bool AnalyticDirty
		{
			get;
			set;
		}
		
		string AnalyticString
		{
			get;
		}
		
		string AnalyticDigit
		{
			get;
		}
	}
}
#endif
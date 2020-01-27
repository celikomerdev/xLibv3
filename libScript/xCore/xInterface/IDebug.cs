#if xLibv3
namespace xLib
{
	public interface IDebug
	{
		UnityEngine.Object UnityObject
		{
			get;
		}
		
		bool CanDebug
		{
			get;
			set;
		}
	}
}
#endif
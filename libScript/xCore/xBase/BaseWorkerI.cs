#if xLibv3
namespace xLib
{
	public interface BaseWorkerI
	{
		UnityEngine.Object UnityObject
		{
			get;
		}
		
		string ViewId
		{
			get;
			set;
		}
		
		void CheckErrors();
	}
}
#endif
#if xLibv3
namespace xLib
{
	public interface BaseWorkerI
	{
		UnityEngine.Object UnityObject
		{
			get;
		}
		
		void CheckErrors();
	}
}
#endif
#if xLibv3
namespace xLib
{
	public interface BaseWorkerI : IDebug
	{
		string ViewId
		{
			get;
			set;
		}
		
		void CheckErrors();
	}
}
#endif
#if xLibv3
namespace xLib
{
	public interface BaseRegisterI : BaseWorkerI
	{
		int Order
		{
			get;
			set;
		}
		
		bool OnRegister
		{
			get;
			set;
		}
	}
}
#endif
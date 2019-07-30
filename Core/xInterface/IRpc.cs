#if xLibv3
namespace xLib
{
	public interface IRpc
	{
		bool UseRpc
		{
			set;
		}
		
		string RpcTarget
		{
			set;
		}
	}
}
#endif
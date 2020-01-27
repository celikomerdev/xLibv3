#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public interface ICall
	{
		void ListenerCall(bool register,UnityAction<object> call,string viewId,int order,bool onRegister=false,BaseWorkerI worker=null);
	}
}
#endif
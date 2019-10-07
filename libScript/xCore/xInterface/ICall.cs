#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public interface ICall
	{
		void ListenerCall(bool register,UnityAction<object> call,int order=0,bool onRegister=false);
		void ListenerEditor(bool addition,BaseActiveM call);
	}
}
#endif
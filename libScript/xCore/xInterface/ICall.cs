﻿#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public interface ICall
	{
		void ListenerCall(bool register,UnityAction<object> call,bool onRegister=false);
		void ListenerEditor(bool addition,BaseActiveM call);
	}
}
#endif
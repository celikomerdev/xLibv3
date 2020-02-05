#if xLibv3
using UnityEngine;

namespace xLib.xValueClass.Listener
{
	// public abstract class OnValue<T> : BaseRegisterM
	public abstract class OnValue : BaseRegisterM
	{
		//TODO
		//private IValue<T>[] target = new IValue<T>[0];
		[SerializeField]internal bool forceClient = false;
		
		protected string lastViewId = string.Empty;
		protected void TryForceClient()
		{
			if(!forceClient) return;
			lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = string.Empty;
		}
		
		protected void TryRestoreLastClient()
		{
			if(!forceClient) return;
			ViewCore.CurrentId = lastViewId;
		}
	}
}
#endif
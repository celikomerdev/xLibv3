#if xLibv3
using UnityEngine;

namespace xLib.xValueClass.Listener
{
	public abstract class OnValue : BaseRegisterM
	{
		[SerializeField]internal bool forceClient;
		
		protected void TryForceClient()
		{
			if(!forceClient) return;
			lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
		}
		
		protected void TryRestoreLastClient()
		{
			if(!forceClient) return;
			ViewCore.CurrentId = lastViewId;
		}
	}
}
#endif
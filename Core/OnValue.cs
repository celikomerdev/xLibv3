#if xLibv3
using UnityEngine;

namespace xLib.xValueClass.Listener
{
	public abstract class OnValue : BaseRegisterM
	{
		[SerializeField]internal bool forceClient;
		
		private string lastClient = null;
		protected void TryForceClient()
		{
			if(!forceClient) return;
			lastClient = ViewCore.CurrentId;
			ViewCore.CurrentId = "0";
		}
		
		protected void TryRestoreLastClient()
		{
			if(!forceClient) return;
			ViewCore.CurrentId = lastClient;
			lastClient = null;
		}
	}
}
#endif
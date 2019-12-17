#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolApplication
{
	public class ApplicationInternetReachability : BaseTickM
	{
		public NetworkReachability networkReachability = NetworkReachability.NotReachable;
		public EventBool eventBool;
		
		#region Mono
		protected override void Tick(float tickTime)
		{
			bool result = (Application.internetReachability == networkReachability);
			eventBool.Invoke(result);
		}
		#endregion
	}
}
#endif
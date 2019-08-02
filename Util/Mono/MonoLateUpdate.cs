#if DiscardxLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoLateUpdate : BaseM
	{
		public EventUnity lateUpdate;
		
		private void LateUpdate()
		{
			lateUpdate.Invoke();
		}
	}
}
#endif
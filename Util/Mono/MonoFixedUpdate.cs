#if DiscardxLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoFixedUpdate : BaseM
	{
		public EventUnity fixedUpdate;
		
		private void FixedUpdate()
		{
			fixedUpdate.Invoke();
		}
	}
}
#endif
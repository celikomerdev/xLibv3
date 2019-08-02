#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoUpdate : BaseM
	{
		[SerializeField]private bool unscaled;
		[SerializeField]private EventFloat deltaTime;
		
		private void Update()
		{
			if(unscaled) deltaTime.Invoke(Time.unscaledDeltaTime);
			else deltaTime.Invoke(Time.deltaTime);
		}
	}
}
#endif
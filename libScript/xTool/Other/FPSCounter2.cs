#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class FPSCounter2 : BaseMainM
	{
		[SerializeField]private float intervalTime = 1f;
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		#region Mono
		private void Update()
		{
			Call();
		}
		#endregion
		
		
		#region Fps
		private float fps;
		private int totalFrame;
		private float totalTime;
		private void Call()
		{
			totalFrame++;
			totalTime += Time.unscaledDeltaTime;
			
			fps += ((1f/Time.unscaledDeltaTime)-fps)/totalFrame;
			if(totalTime<intervalTime) return;
			totalTime = 0;
			totalFrame = 0;
			
			eventFloat.Invoke(fps);
		}
		#endregion
	}
}
#endif
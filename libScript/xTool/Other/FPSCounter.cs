#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool
{
	public class FPSCounter : BaseMainM
	{
		[SerializeField]private float intervalTime = 1f;
		[SerializeField]private float lerp = 1f;
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
			
			if(totalTime<intervalTime) return;
			fps = Mathf.Lerp(fps,totalFrame/totalTime,lerp*totalTime);
			totalFrame = 0;
			totalTime = 0;
			
			eventFloat.Invoke(fps);
		}
		#endregion
	}
}
#endif
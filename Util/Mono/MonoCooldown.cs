#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xNew
{
	public class MonoCooldown : BaseWorkM
	{
		[SerializeField]private bool canMessage;
		[SerializeField]private float lastTime;
		[SerializeField]private int intervalTime;
		[SerializeField]private EventBool eventBool;
		
		
		public void Call()
		{
			if(!CanWork) return;
			eventBool.Invoke(TimeReady);
		}
		
		private bool TimeReady
		{
			get
			{
				float remainingTime = (lastTime+intervalTime)-Time.realtimeSinceStartup;
				if(remainingTime > 0)
				{
					if(canMessage) StPopupBar.Message(string.Format(MnLocalize.GetValue("please wait {0} seconds"),remainingTime.ToString("F0")));
					return false;
				}
				lastTime = Time.realtimeSinceStartup;
				return true;
			}
		}
	}
}
#endif
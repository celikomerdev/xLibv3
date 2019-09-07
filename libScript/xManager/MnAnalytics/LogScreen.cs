#if xLibv3
using UnityEngine;

namespace xLib.ToolManager
{
	public class LogScreen : BaseMainM
	{
		[SerializeField]private string screen = "null";
		public string Screen
		{
			get
			{
				return screen;
			}
			set
			{
				this.screen = value;
			}
		}
		
		public void LogName()
		{
			StAnalytics.LogScreen(Screen);
		}
	}
}
#endif
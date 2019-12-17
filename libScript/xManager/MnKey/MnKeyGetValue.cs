#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolManager
{
	public class MnKeyGetValue : BaseMainM
	{
		public string key;
		public EventString eventString;
		
		private void Start()
		{
			eventString.Invoke(MnKey.GetValue(key));
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolManager
{
	public class MnKeyGetValue : BaseMainM
	{
		[SerializeField]private string key = "";
		[SerializeField]private EventString eventString = new EventString();
		
		private void Start()
		{
			eventString.Invoke(MnKey.GetValue(key));
		}
	}
}
#endif
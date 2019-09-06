#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetKeyDown : BaseM
	{
		[SerializeField]private string key = "";
		[SerializeField]private EventBool eventBool = new EventBool();
		
		private void Update()
		{
			if(Input.GetKeyDown(key)) eventBool.Invoke(true);
			else if(Input.GetKeyUp(key)) eventBool.Invoke(false);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetKeyUp : BaseM
	{
		[SerializeField]private string key;
		[SerializeField]private EventBool eventBool;
		
		private void Update()
		{
			if(Input.GetKeyDown(key)) eventBool.Invoke(false);
			else if(Input.GetKeyUp(key)) eventBool.Invoke(true);
		}
	}
}
#endif
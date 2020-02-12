#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetKeyDown : BaseTickNodeM
	{
		[SerializeField]private string key = "";
		[SerializeField]private EventBool eventBool = new EventBool();
		
		protected override void Tick(float tickTime)
		{
			if(Input.GetKeyDown(key)) eventBool.Invoke(true);
			else if(Input.GetKeyUp(key)) eventBool.Invoke(false);
		}
	}
}
#endif
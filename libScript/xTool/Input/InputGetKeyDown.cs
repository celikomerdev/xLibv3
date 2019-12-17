#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetKeyDown : BaseTickNodeM
	{
		[SerializeField]private string key = "";
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventKeyDown = new EventBool();
		
		protected override void Tick(float tickTime)
		{
			if(Input.GetKeyDown(key)) eventKeyDown.Invoke(true);
			else if(Input.GetKeyUp(key)) eventKeyDown.Invoke(false);
		}
	}
}
#endif
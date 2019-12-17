#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetKeyUp : BaseTickNodeM
	{
		[SerializeField]private string key = "";
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventKeyUp = new EventBool();
		
		protected override void Tick(float tickTime)
		{
			if(Input.GetKeyDown(key)) eventKeyUp.Invoke(false);
			else if(Input.GetKeyUp(key)) eventKeyUp.Invoke(true);
		}
	}
}
#endif
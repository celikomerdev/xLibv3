#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetAxis : BaseTickNodeM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("key")]
		[SerializeField]private string axis = "";
		[UnityEngine.Serialization.FormerlySerializedAs("eventFloat")]
		[SerializeField]private EventFloat eventAxisValue = new EventFloat();
		
		protected override void Tick(float tickTime)
		{
			eventAxisValue.Invoke(Input.GetAxis(axis));
		}
	}
}
#endif
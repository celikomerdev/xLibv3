#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTool.xInput
{
	public class InputGetAxis : BaseTickNodeM
	{
		[SerializeField]private string key = "";
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		protected override void Tick(float tickTime)
		{
			eventFloat.Invoke(Input.GetAxis(key));
		}
	}
}
#endif
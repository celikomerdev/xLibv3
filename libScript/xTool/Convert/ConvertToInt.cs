#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolConvert
{
	public class ConvertToInt : BaseM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventInt eventResult = new EventInt();
		
		public void FromByte(byte value)
		{
			eventResult.Invoke(value);
		}
		
		public void FromFloat(float value)
		{
			eventResult.Invoke(Mathf.RoundToInt(value));
		}
		
		public void FromString(string value)
		{
			int result = 0;
			if(!int.TryParse(value,out result)) return;
			eventResult.Invoke(result);
		}
	}
}
#endif
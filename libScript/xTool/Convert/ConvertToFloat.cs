#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class ConvertToFloat : BaseMainM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventFloat eventResult = new EventFloat();
		
		public void FromByte(byte value)
		{
			eventResult.Invoke(value);
		}
		
		public void FromInt(int value)
		{
			eventResult.Invoke(value);
		}
		
		public void FromString(string value)
		{
			float result = 0;
			if(!float.TryParse(value,out result)) return;
			eventResult.Invoke(result);
		}
	}
}
#endif
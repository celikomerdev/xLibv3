#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class ConvertToLong : BaseM
	{
		private long result = 0;
		private long Result
		{
			set
			{
				if(result == value) return;
				result = value;
				eventResult.Invoke(result);
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventLong eventResult;
		
		public void FromByte(byte value)
		{
			Result = value;
		}
		
		public void FromInt(int value)
		{
			Result = value;
		}
		
		public void FromFloat(float value)
		{
			Result = Mathf.RoundToInt(value);
		}
		
		public void FromString(string value)
		{
			long temp = 0;
			if(!long.TryParse(value,out temp)) return;
			Result = temp;
		}
	}
}
#endif
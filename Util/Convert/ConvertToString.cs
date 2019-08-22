#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class ConvertToString : BaseM
	{
		[SerializeField]private string format = "F0";
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventString eventResult;
		private string result = "";
		private string Result
		{
			set
			{
				if(result == value) return;
				result = value;
				eventResult.Invoke(result);
			}
		}
		
		public void FromByte(byte value)
		{
			Result = value.ToString(format);
		}
		
		public void FromInt(int value)
		{
			Result = value.ToString(format);
		}
		
		public void FromFloat(float value)
		{
			Result = value.ToString(format);
		}
		
		public void FromTimeSpan(TimeSpan value)
		{
			Result = value.ToStringCustom(format);
		}
		
		public void FromTimeTick(long value)
		{
			FromTimeSpan(TimeSpan.FromTicks(value));
		}
		
		public void FromWWW(WWW value)
		{
			Result = value.text;
		}
	}
}
#endif
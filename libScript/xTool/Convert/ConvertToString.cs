#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class ConvertToString : BaseMainM
	{
		[SerializeField]private string format = @"{0:F0}";
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
		
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventString eventResult = new EventString();
		
		public void FromByte(byte value)
		{
			Result = string.Format(@format,value);
		}
		
		public void FromInt(int value)
		{
			Result = string.Format(@format,value);
		}
		
		public void FromFloat(float value)
		{
			Result = string.Format(@format,value);
		}
		
		public void FromTimeSpan(TimeSpan value)
		{
			Result = string.Format(@format,(xTimeSpan)value);
		}
		
		#if ModWebWWW
		public void FromWWW(WWW value)
		{
			Result = string.Format(@format,value);
		}
		#endif
		
		#region FromTime
		public void FromTimeTick(long value)
		{
			FromTimeSpan(TimeSpan.FromTicks(value));
		}
		
		public void FromTimeSecond(float value)
		{
			FromTimeSpan(TimeSpan.FromSeconds(value));
		}
		
		public void FromTimeSecond(int value)
		{
			FromTimeSpan(TimeSpan.FromSeconds(value));
		}
		#endregion
	}
}
#endif
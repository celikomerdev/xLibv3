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
		[SerializeField]private EventString eventConvert;
		private void OnConvert(string value)
		{
			eventConvert.Invoke(value);
		}
		
		public void FromByte(byte value)
		{
			string result = value.ToString(format);
			OnConvert(result);
		}
		
		public void FromInt(int value)
		{
			string result = value.ToString(format);
			OnConvert(result);
		}
		
		public void FromFloat(float value)
		{
			string result = value.ToString(format);
			OnConvert(result);
		}
		
		public void FromTimeSpan(TimeSpan value)
		{
			string result = value.ToStringCustom(format);
			OnConvert(result);
		}
		
		public void FromTimeTick(long value)
		{
			FromTimeSpan(TimeSpan.FromTicks(value));
		}
		
		public void FromWWW(WWW value)
		{
			string result = value.text;
			OnConvert(result);
		}
	}
}
#endif
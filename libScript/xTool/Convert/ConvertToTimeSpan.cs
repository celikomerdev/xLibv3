#if xLibv2
using System;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolConvert
{
	public class ConvertToTimeSpan : BaseM
	{
		private int days;
		public int Days
		{
			get
			{
				return days;
			}
			set
			{
				if(days == value) return;
				days = value;
				FromSet();
			}
		}
		
		private int hours;
		public int Hours
		{
			get
			{
				return hours;
			}
			set
			{
				if(hours == value) return;
				hours = value;
				FromSet();
			}
		}
		
		private int minutes;
		public int Minutes
		{
			get
			{
				return minutes;
			}
			set
			{
				if(minutes == value) return;
				minutes = value;
				FromSet();
			}
		}
		
		private int seconds;
		public int Seconds
		{
			get
			{
				return seconds;
			}
			set
			{
				if(seconds == value) return;
				seconds = value;
				FromSet();
			}
		}
		
		private int milliseconds;
		public int Milliseconds
		{
			get
			{
				return milliseconds;
			}
			set
			{
				if(milliseconds == value) return;
				milliseconds = value;
				FromSet();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventTimeSpan eventResult = new EventTimeSpan();
		private void FromSet()
		{
			TimeSpan timeSpan = new TimeSpan(days,hours,minutes,seconds,milliseconds);
			eventResult.Invoke(timeSpan);
		}
		
		private void FromTicks(long value)
		{
			TimeSpan timeSpan = new TimeSpan(value);
			eventResult.Invoke(timeSpan);
		}
	}
}
#endif
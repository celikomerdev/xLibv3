#if xLibv3
using UnityEngine;
using UnityEngine.Serialization;

namespace xLib.ToolManager
{
	public class LogEvent : BaseMainM
	{
		[FormerlySerializedAs("key")]
		[SerializeField]private string group = "group";
		public string Group
		{
			get
			{
				return group;
			}
			set
			{
				this.group = value;
			}
		}
		
		[FormerlySerializedAs("label")]
		[SerializeField]private string key = "key";
		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				this.key = value;
			}
		}
		
		[SerializeField]private double digit = 0;
		public double Digit
		{
			get
			{
				return digit;
			}
			set
			{
				this.digit = value;
			}
		}
		
		[SerializeField]private string data = "data";
		public string Data
		{
			get
			{
				return data;
			}
			set
			{
				this.data = value;
			}
		}
		
		public void Call()
		{
			StAnalytics.LogEvent(group:group,key:key,digit:digit,data:data);
		}
	}
}
#endif
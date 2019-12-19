#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib.ToolManager
{
	public class LogEvent : BaseMainM
	{
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
		
		[SerializeField]private string label = "label";
		public string Label
		{
			get
			{
				return label;
			}
			set
			{
				this.label = value;
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
			StAnalytics.LogEvent(key:key,label:label,digit:digit,data:new Dictionary<string,object>{{"data",data}});
		}
	}
}
#endif
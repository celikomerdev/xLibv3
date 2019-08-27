#if xLibv3
using UnityEngine;

namespace xLib.ToolManager
{
	public class LogEvent : BaseM
	{
		[SerializeField]private string category = "category";
		public string Category
		{
			get
			{
				return category;
			}
			set
			{
				this.category = value;
			}
		}
		
		[SerializeField]private string action = "action";
		public string Action
		{
			get
			{
				return action;
			}
			set
			{
				this.action = value;
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
		
		[SerializeField]private string value = "0";
		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}
		
		public void Call()
		{
			StAnalytics.LogEvent(Category,Action,Label,Value);
		}
	}
}
#endif
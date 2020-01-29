#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class ReplaceString : BaseWorkM
	{
		#region Property
		[SerializeField]private string left = "";
		public string Left
		{
			get
			{
				return left;
			}
			set
			{
				if(left == value) return;
				left = value;
				Operation();
			}
		}
		
		[SerializeField]private string right = "";
		public string Right
		{
			get
			{
				return right;
			}
			set
			{
				if(right == value) return;
				right = value;
				Operation();
			}
		}
		
		[SerializeField]private string format = "";
		public string Format
		{
			get
			{
				return format;
			}
			set
			{
				if(Format == value) return;
				format = value;
				Operation();
			}
		}
		#endregion
		
		[SerializeField]private EventString eventResult = new EventString();
		private void Operation()
		{
			eventResult.Invoke(format.Replace(left,right));
		}
	}
}
#endif
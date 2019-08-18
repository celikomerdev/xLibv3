#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolLocalize
{
	public class MnLocalizeGetValue : BaseActiveM
	{
		#region Value
		[SerializeField]private string value = "";
		public string Value
		{
			set
			{
				this.value = value;
				GetValue();
			}
		}
		#endregion
		
		protected override void OnEnabled()
		{
			GetValue();
		}
		
		[SerializeField]private EventString eventString = new EventString();
		public void GetValue()
		{
			eventString.Invoke(MnLocalize.GetValue(value));
		}
	}
}
#endif
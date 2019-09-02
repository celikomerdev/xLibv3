#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolLocalize
{
	public class MnLocalizeGetValue : BaseRegisterM
	{
		[SerializeField]private string value = "";
		public string Value
		{
			set
			{
				if(this.value == value) return;
				this.value = value;
				Work();
			}
		}
		
		[SerializeField]private EventString eventString = new EventString();
		private void Work()
		{
			eventString.Invoke(MnLocalize.GetValue(value));
		}
		
		protected override bool OnRegister(bool value)
		{
			MnLocalize.ins.eventLocalize.Listener(value,(Void)=>Work(),true);
			return value;
		}
	}
}
#endif
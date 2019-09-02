#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnTestGroupGetGroup : BaseRegisterM
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
		
		[SerializeField]private EventInt eventGroup = new EventInt();
		private void Work()
		{
			eventGroup.Invoke(MnTestGroup.GetGroup(value));
		}
		
		protected override bool OnRegister(bool value)
		{
			if(baseRegister.onRegister) Work();
			return value;
		}
	}
}
#endif
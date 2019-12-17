#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnTestGroupGetGroup : BaseRegisterM
	{
		[SerializeField]private string key = "";
		public string Key
		{
			set
			{
				if(key == value) return;
				key = value;
				Work();
			}
		}
		
		[SerializeField]private EventInt eventGroup = new EventInt();
		private void Work()
		{
			eventGroup.Invoke(MnTestGroup.GetGroup(key));
		}
		
		protected override bool OnRegister(bool value)
		{
			if(baseRegister.onRegister) Work();
			return value;
		}
	}
}
#endif
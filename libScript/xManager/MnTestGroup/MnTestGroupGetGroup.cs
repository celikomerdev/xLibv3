#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xValueClass;

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
				Work(new Void());
			}
		}
		
		[SerializeField]private EventInt eventGroup = new EventInt();
		private void Work(Void value)
		{
			int group = MnTestGroup.GetGroup(key);
			if(CanDebug) Debug.Log($"{this.name}:GetGroup:{key}:{group}",this);
			eventGroup.Invoke(group);
		}
		
		protected override bool TryRegister(bool register)
		{
			MnTestGroup.onRefreshGroups.Listener(register:register,call:Work,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister,worker:this);
			return register;
		}
	}
}
#endif
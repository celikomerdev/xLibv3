#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueString : OnValue
	{
		[SerializeField]private NodeString[] target = new NodeString[0];
		[SerializeField]private MonoString[] targetMono = new MonoString[0];
		[SerializeField]private EventString eventString = new EventString();
		
		public void OnCall(string value)
		{
			TryForceClient();
			if(CanDebug) Debug.Log($"{this.name}:OnCall:view:{ViewCore.CurrentId}:value:{value}",this);
			eventString.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool TryRegister(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if(!target[i]) continue;
				target[i].Listener(register:register,call:OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister,worker:this);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				if(!targetMono[i]) continue;
				targetMono[i].Listener(register:register,call:OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister,worker:this);
			}
			return register;
		}
	}
}
#endif
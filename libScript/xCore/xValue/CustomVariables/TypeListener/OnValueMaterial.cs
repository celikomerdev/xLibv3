#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueMaterial : OnValue
	{
		[SerializeField]private NodeMaterial[] target = new NodeMaterial[0];
		[SerializeField]private MonoMaterial[] targetMono = new MonoMaterial[0];
		[SerializeField]private EventMaterial eventMaterial = new EventMaterial();
		
		public void OnCall(Material value)
		{
			TryForceClient();
			if(CanDebug) Debug.Log($"{this.name}:OnCall:view:{ViewCore.CurrentId}:value:{value}",this);
			eventMaterial.Invoke(value);
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
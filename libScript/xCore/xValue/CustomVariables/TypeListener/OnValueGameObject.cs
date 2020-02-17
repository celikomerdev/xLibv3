#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueGameObject : OnValue
	{
		[SerializeField]private NodeGameObject[] target = new NodeGameObject[0];
		[SerializeField]private MonoGameObject[] targetMono = new MonoGameObject[0];
		[SerializeField]private EventGameObject eventGameObject = new EventGameObject();
		
		public void OnCall(GameObject value)
		{
			TryForceClient();
			if(CanDebug) Debug.Log($"{this.name}:OnCall:view:{ViewCore.CurrentId}:value:{value}",this);
			eventGameObject.Invoke(value);
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
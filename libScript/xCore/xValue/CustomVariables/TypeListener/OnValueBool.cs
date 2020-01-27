#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueBool : OnValue
	{
		[SerializeField]private NodeBool[] target = new NodeBool[0];
		[SerializeField]private MonoBool[] targetMono = new MonoBool[0];
		[SerializeField]private EventBool eventBool = new EventBool();
		
		public void OnCall(bool value)
		{
			TryForceClient();
			if(CanDebug) Debug.Log($"{this.name}:OnCall:view:{ViewCore.CurrentId}:value:{value}",this);
			eventBool.Invoke(value);
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
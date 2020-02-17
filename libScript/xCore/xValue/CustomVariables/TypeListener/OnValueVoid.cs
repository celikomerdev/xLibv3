﻿#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueVoid : OnValue
	{
		[SerializeField]private NodeVoid[] target = new NodeVoid[0];
		[SerializeField]private MonoVoid[] targetMono = new MonoVoid[0];
		[SerializeField]private EventUnity eventUnity = new EventUnity();
		
		public void OnCall(Void value)
		{
			TryForceClient();
			if(CanDebug) Debug.Log($"{this.name}:OnCall:view:{ViewCore.CurrentId}:value:{value}",this);
			eventUnity.Invoke();
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
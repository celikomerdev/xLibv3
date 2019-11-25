﻿#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventMaterial.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool OnRegister(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if(!target[i]) continue;
				target[i].Listener(register,OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
				target[i].ListenerEditor(register,this);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				if(!targetMono[i]) continue;
				targetMono[i].Listener(register,OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
				targetMono[i].ListenerEditor(register,this);
			}
			return register;
		}
	}
}
#endif
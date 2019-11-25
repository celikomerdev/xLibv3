﻿#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueTexture2D : OnValue
	{
		[SerializeField]private NodeTexture2D[] target = new NodeTexture2D[0];
		[SerializeField]private MonoTexture2D[] targetMono = new MonoTexture2D[0];
		[SerializeField]private EventTexture eventTexture = new EventTexture();
		
		public void OnCall(Texture2D value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventTexture.Invoke(value);
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
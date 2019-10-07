#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewId,value);
			eventUnity.Invoke();
			TryRestoreLastClient();
		}
		
		protected override bool OnRegister(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if(!target[i]) continue;
				target[i].ListenerEditor(register,this);
				target[i].Listener(register,call:OnCall,order:baseRegister.order,onRegister:baseRegister.onRegister);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				if(!targetMono[i]) continue;
				targetMono[i].ListenerEditor(register,this);
				targetMono[i].Listener(register,call:OnCall,order:baseRegister.order,onRegister:baseRegister.onRegister);
			}
			return register;
		}
	}
}
#endif
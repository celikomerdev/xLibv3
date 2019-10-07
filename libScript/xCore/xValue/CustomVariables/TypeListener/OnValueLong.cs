#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueLong : OnValue
	{
		[SerializeField]private NodeLong[] target = new NodeLong[0];
		[SerializeField]private MonoLong[] targetMono = new MonoLong[0];
		[SerializeField]private EventLong eventLong = new EventLong();
		
		public void OnCall(long value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventLong.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool OnRegister(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if(!target[i]) continue;
				target[i].ListenerEditor(register,this);
				target[i].Listener(register,call:OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				if(!targetMono[i]) continue;
				targetMono[i].ListenerEditor(register,this);
				targetMono[i].Listener(register,call:OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
			}
			return register;
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueSprite : OnValue
	{
		[SerializeField]private NodeSprite[] target = new NodeSprite[0];
		[SerializeField]private MonoSprite[] targetMono = new MonoSprite[0];
		[SerializeField]private EventSprite eventSprite = new EventSprite();
		
		public void OnCall(Sprite value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewId,value);
			eventSprite.Invoke(value);
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
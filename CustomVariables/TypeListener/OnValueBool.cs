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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventBool.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool OnRegister(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if(!target[i]) continue;
				target[i].ListenerEditor(register,this);
				target[i].Listener(register,OnCall,baseRegister.onRegister);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				if(!targetMono[i]) continue;
				targetMono[i].ListenerEditor(register,this);
				targetMono[i].Listener(register,OnCall,baseRegister.onRegister);
			}
			return register;
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueBool : OnValue
	{
		[SerializeField]private NodeBool[] target;
		[SerializeField]private MonoBool[] targetMono;
		[SerializeField]private EventBool eventBool;
		
		public void OnCall(bool value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventBool.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool Register(bool register)
		{
			for (int i = 0; i < target.Length; i++)
			{
				target[i].ListenerEditor(register,this);
				target[i].Listener(register,OnCall,baseRegister.onRegister);
			}
			for (int i = 0; i < targetMono.Length; i++)
			{
				targetMono[i].ListenerEditor(register,this);
				targetMono[i].Listener(register,OnCall,baseRegister.onRegister);
			}
			return register;
		}
	}
}
#endif
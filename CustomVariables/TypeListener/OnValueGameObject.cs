#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
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
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventGameObject.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool Register(bool register)
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
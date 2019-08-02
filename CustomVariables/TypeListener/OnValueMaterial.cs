#if xLibv3
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
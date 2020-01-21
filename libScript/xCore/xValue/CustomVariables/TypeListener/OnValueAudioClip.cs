#if xLibv3
#if ModAudio
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueAudioClip : OnValue
	{
		[SerializeField]private NodeAudioClip[] target = new NodeAudioClip[0];
		[SerializeField]private MonoAudioClip[] targetMono = new MonoAudioClip[0];
		[SerializeField]private EventAudioClip eventAudioClip = new EventAudioClip();
		
		public void OnCall(AudioClip value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}:{1}",ViewCore.CurrentId,value);
			eventAudioClip.Invoke(value);
			TryRestoreLastClient();
		}
		
		protected override bool TryRegister(bool register)
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
#endif
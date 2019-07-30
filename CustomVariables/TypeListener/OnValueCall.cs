#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueCall : OnValue
	{
		[SerializeField]private Object[] target;
		[SerializeField]private EventUnity eventUnity;
		
		public void OnCall()
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}",ViewCore.CurrentId);
			eventUnity.Invoke();
			TryRestoreLastClient();
		}
		
		protected override bool Register(bool register)
		{
			ICall[] array = target.GetGenericsArray<ICall>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ListenerEditor(register,this);
				array[i].ListenerCall(register,OnCall,baseRegister.onRegister);
			}
			return register;
		}
	}
}
#endif
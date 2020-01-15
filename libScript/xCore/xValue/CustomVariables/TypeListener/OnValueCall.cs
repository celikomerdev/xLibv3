#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xValueClass.Listener
{
	public class OnValueCall : OnValue
	{
		[SerializeField]private Object[] target = new Object[0];
		[SerializeField]private EventUnity eventUnity = new EventUnity();
		
		public void OnCall(object value)
		{
			TryForceClient();
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCall:{0}",ViewCore.CurrentId);
			eventUnity.Invoke();
			TryRestoreLastClient();
		}
		
		protected override bool OnRegister(bool register)
		{
			ICall[] array = target.GetGenericsArray<ICall>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ListenerCall(register:register,call:OnCall,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister,worker:this);
			}
			return register;
		}
	}
}
#endif
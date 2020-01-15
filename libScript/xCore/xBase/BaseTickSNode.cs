#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public abstract class BaseTickNodeS : BaseTickS
	{
		[SerializeField]private NodeFloat tickTime = null;
		
		#region Custom
		protected override bool OnRegister(bool register)
		{
			tickTime.Listener(register:register,call:TickMulti,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister,worker:this);
			return register;
		}
		#endregion
	}
}
#endif
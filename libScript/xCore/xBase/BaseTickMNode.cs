#if xLibv3
using xLib.xNode.NodeObject;

namespace xLib
{
	public abstract class BaseTickNodeM : BaseTickM
	{
		public NodeFloat tickTime;
		
		#region Custom
		protected override bool OnRegister(bool register)
		{
			tickTime.ListenerEditor(register,this);
			tickTime.Listener(register,call:TickMulti,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
			return register;
		}
		#endregion
	}
}
#endif
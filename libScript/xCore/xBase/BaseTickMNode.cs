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
			tickTime.Listener(register,TickMulti,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
			tickTime.ListenerEditor(register,this);
			return register;
		}
		#endregion
	}
}
#endif
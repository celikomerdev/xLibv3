#if xLibv3
using xLib.xNode.NodeObject;

namespace xLib
{
	public abstract class BaseTickNodeM : BaseTickM
	{
		public NodeFloat tickTime;
		
		#region Custom
		protected override bool Register(bool register)
		{
			tickTime.ListenerEditor(register,this);
			tickTime.Listener(register,TickMulti,baseRegister.onRegister);
			return register;
		}
		#endregion
	}
}
#endif
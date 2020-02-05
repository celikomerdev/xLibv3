#if xLibv3
namespace xLib
{
	public abstract class BaseTickM : BaseRegisterM
	{
		#region Virtual
		protected void TickMulti(float tickTime)
		{
			string tempViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			Tick(tickTime);
			ViewCore.CurrentId = tempViewId;
		}
		protected virtual void Tick(float tickTime){}
		#endregion
	}
}
#endif
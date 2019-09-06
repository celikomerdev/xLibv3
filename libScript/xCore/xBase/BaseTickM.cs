#if xLibv3
namespace xLib
{
	public abstract class BaseTickM : BaseRegisterM
	{
		#region Virtual
		protected void TickMulti(float tickTime)
		{
			ViewIdApplyFast();
			Tick(tickTime);
			ViewIdRestoreFast();
		}
		protected virtual void Tick(float tickTime){}
		#endregion
	}
}
#endif
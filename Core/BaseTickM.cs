#if xLibv3
namespace xLib
{
	public abstract class BaseTickM : BaseRegisterM
	{
		#region Virtual
		protected void TickMulti(float tickTime)
		{
			ApplyViewIdTick();
			Tick(tickTime);
			ApplyLastIdTick();
		}
		protected virtual void Tick(float tickTime){}
		#endregion
	}
}
#endif
#if xLibv3
namespace xLib
{
	public abstract class BaseTickS : BaseRegisterS
	{
		private string tempId = "Client";
		#region Virtual
		protected void TickMulti(float tickTime)
		{
			tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			Tick(tickTime);
			ViewCore.CurrentId = tempId;
		}
		protected virtual void Tick(float tickTime){}
		#endregion
	}
}
#endif
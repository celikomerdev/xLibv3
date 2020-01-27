#if xLibv3
namespace xLib
{
	public abstract class BaseWorkerM : BaseWorkM, BaseWorkerI
	{
		internal View view = null;
		protected bool isMy = true;
		private string viewId = "Client";
		protected string lastViewId = "Client";
		public string ViewId
		{
			get
			{
				return viewId;
			}
			set
			{
				viewId = value;
			}
		}
		
		protected void FindView()
		{
			if(!view) GetComponentInParent<View>();
			if(!view) return;
			
			view.FindId();
			ViewId = view.Id;
			isMy = view.IsMy;
		}
		
		public virtual void CheckErrors()
		{
			#if UNITY_EDITOR
			if(ViewCore.CurrentId != ViewId) xLogger.LogExceptionFormat(this,$"{this.name}:CurrentId:{0}:ViewId:{1}",ViewCore.CurrentId,ViewId);
			#endif
		}
	}
}
#endif
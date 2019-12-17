#if xLibv3
namespace xLib
{
	public abstract class BaseWorkerM : BaseWorkM
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
		
		#if UNITY_EDITOR
		public virtual void CheckErrors()
		{
			if(ViewCore.CurrentId != ViewId) xDebug.LogExceptionFormat(this,this.name+":CurrentId:{0}:viewId:{1}",ViewCore.CurrentId,ViewId);
		}
		#else
		public virtual void CheckErrors(){}
		#endif
	}
}
#endif
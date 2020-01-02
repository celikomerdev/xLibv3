#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseWorkerS : BaseWorkS
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
			// if(!view) GetComponentInParent<View>();
			if(!view) return;
			
			view.FindId();
			ViewId = view.Id;
			isMy = view.IsMy;
		}
		
		protected void ViewIdApplyFast()
		{
			lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
		}
		protected void ViewIdApply()
		{
			if(ViewCore.canDebug && ViewCore.CurrentId != ViewId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,ViewId);
			ViewIdApplyFast();
		}
		
		protected void ViewIdRestoreFast()
		{
			ViewCore.CurrentId = lastViewId;
		}
		protected void ViewIdRestore()
		{
			if(ViewCore.canDebug && ViewCore.CurrentId != lastViewId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,lastViewId);
			ViewIdRestoreFast();
		}
		
		#if UNITY_EDITOR
		public virtual void CheckErrors()
		{
			if(ViewCore.CurrentId != ViewId) xLogger.LogExceptionFormat(this,this.name+":CurrentId:{0}:viewId:{1}",ViewCore.CurrentId,ViewId);
		}
		#else
		public virtual void CheckErrors(){}
		#endif
	}
}
#endif
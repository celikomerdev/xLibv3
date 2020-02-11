#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseWorkerM : BaseWorkM, BaseWorkerI
	{
		protected bool isMy = true;
		
		private string viewId = string.Empty;
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
		
		internal View view = null;
		protected virtual void FindView()
		{
			if(!view) view = GetComponentInParent<View>();
			if(!view) return;
			
			view.FindId();
			ViewId = view.Id;
			isMy = view.IsMy;
		}
		
		public virtual void CheckErrors()
		{
			#if UNITY_EDITOR
			if(ViewCore.CurrentId != ViewId) Debug.LogException(new UnityException($"{this.name}:CurrentId:{ViewCore.CurrentId}:ViewId:{ViewId}"),this);
			#endif
		}
	}
}
#endif
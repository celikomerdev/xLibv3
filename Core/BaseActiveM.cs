#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseActiveM : BaseWorkM
	{
		public BaseActiveInfo activeInfo = new BaseActiveInfo();
		
		#region Mono
		protected virtual void Awaked(){}
		internal virtual void Awake()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Awake");
			isStarted = false;
			if(!CanWork) return;
			FindView();
			ApplyViewIdWithDebug();
			
			Awaked();
			
			ApplyLastIdWithDebug();
		}
		
		private bool isStarted;
		protected virtual void Started(){}
		internal virtual void Start()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Start");
			isStarted = false;
			if(!CanWork) return;
			FindView();
			ApplyViewIdWithDebug();
			
			Started();
			
			ApplyLastIdWithDebug();
			
			isStarted = true;
			OnEnable();
		}
		
		protected virtual void OnEnabled(){}
		internal virtual void OnEnable()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnEnable");
			if(!isStarted) return;
			if(!CanWork) return;
			FindView();
			ApplyViewIdWithDebug();
			
			OnEnabled();
			IsActive = true;
			
			ApplyLastIdWithDebug();
		}
		
		protected virtual void OnDisabled(){}
		internal virtual void OnDisable()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisable");
			if(!isStarted) return;
			if(!CanWork) return;
			ApplyViewIdWithDebug();
			
			activeInfo.inDisable = true;
			OnDisabled();
			IsActive = false;
			activeInfo.inDisable = false;
			
			ApplyLastIdWithDebug();
		}
		
		protected virtual void OnDestroyed(){}
		internal virtual void OnDestroy()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDestroy");
			isStarted = false;
			if(!CanWork) return;
			ApplyViewIdWithDebug();
			
			OnDestroyed();
			IsActive = false;
			
			ApplyLastIdWithDebug();
		}
		#endregion
		
		
		#region Custom
		public bool IsActive
		{
			get
			{
				return activeInfo.isActive;
			}
			set
			{
				if(!CanWork) return;
				if(activeInfo.isActive == value) return;
				activeInfo.isActive = value;
				
				if(value) FindView();
				ApplyViewIdWithDebug();
				SetActive(value);
				ApplyLastIdWithDebug();
			}
		}
		protected virtual void SetActive(bool value){}
		#endregion
		
		
		#region GroupId
		internal View view;
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
		
		private void FindView()
		{
			view = GetComponentInParent<View>();
			if(!view) return;
			
			view.FindId();
			ViewId = view.Id;
			isMy = view.IsMy;
		}
		
		protected void ApplyViewId()
		{
			lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
		}
		protected void ApplyViewIdWithDebug()
		{
			if(ViewCore.canDebug && ViewCore.CurrentId != ViewId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,ViewId);
			ApplyViewId();
		}
		
		protected void ApplyLastId()
		{
			ViewCore.CurrentId = lastViewId;
		}
		protected void ApplyLastIdWithDebug()
		{
			if(ViewCore.canDebug && ViewCore.CurrentId != lastViewId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,lastViewId);
			ApplyLastId();
		}
		#endregion
		
		
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
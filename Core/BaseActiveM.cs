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
			ApplyViewId();
			
			Awaked();
			
			ApplyLastId();
		}
		
		private bool isStarted;
		protected virtual void Started(){}
		internal virtual void Start()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Start");
			isStarted = false;
			if(!CanWork) return;
			FindView();
			ApplyViewId();
			
			Started();
			
			ApplyLastId();
			
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
			ApplyViewId();
			
			OnEnabled();
			IsActive = true;
			
			ApplyLastId();
		}
		
		protected virtual void OnDisabled(){}
		internal virtual void OnDisable()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisable");
			if(!isStarted) return;
			if(!CanWork) return;
			ApplyViewId();
			
			activeInfo.inDisable = true;
			OnDisabled();
			IsActive = false;
			activeInfo.inDisable = false;
			
			ApplyLastId();
		}
		
		protected virtual void OnDestroyed(){}
		internal virtual void OnDestroy()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDestroy");
			isStarted = false;
			if(!CanWork) return;
			ApplyViewId();
			
			OnDestroyed();
			IsActive = false;
			
			ApplyLastId();
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
				ApplyViewId();
				SetActive(value);
				ApplyLastId();
			}
		}
		protected virtual void SetActive(bool value){}
		#endregion
		
		
		#region GroupId
		internal View view;
		protected bool isMy = true;
		private string viewId = "0";
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
			//if(MnPlayer.CurrentId == ViewId) return;
			if(ViewCore.canDebug && ViewCore.CurrentId != ViewId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,ViewId);
			lastId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
		}
		
		protected void ApplyViewIdTick()
		{
			//if(MnPlayer.CurrentId == ViewId) return;
			lastId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
		}
		
		protected string lastId = "0";
		protected void ApplyLastId()
		{
			if(ViewCore.canDebug && ViewCore.CurrentId != lastId) Debug.LogFormat(this,"CurrentId:{0}:{1}",ViewCore.CurrentId,lastId);
			ViewCore.CurrentId = lastId;
			lastId = ViewId;
		}
		
		protected void ApplyLastIdTick()
		{
			ViewCore.CurrentId = lastId;
			lastId = ViewId;
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
		
		internal virtual void FillStatic()
		{
			//MnPlayer.CurrentId = viewId;
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseActiveS : BaseWorkerS
	{
		#region Mono
		protected virtual void Awaked(){}
		internal virtual void Awake()
		{
			if(CanDebug) Debug.Log($"{this.name}:Awake",this);
			if(!CanWork) return;
			
			FindView();
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			Awaked();
			ViewCore.CurrentId = tempId;
			
			IsActive = false;
		}
		
		private bool isStarted = false;
		protected virtual void Started(){}
		internal virtual void Start()
		{
			if(CanDebug) Debug.Log($"{this.name}:Start",this);
			if(!CanWork) return;
			
			FindView();
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			Started();
			ViewCore.CurrentId = tempId;
			
			isStarted = true;
			OnEnable();
		}
		
		protected virtual void OnEnabled(){}
		internal virtual void OnEnable()
		{
			if(!isStarted) return;
			if(CanDebug) Debug.Log($"{this.name}:OnEnable",this);
			if(!CanWork) return;
			
			FindView();
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			OnEnabled();
			ViewCore.CurrentId = tempId;
			
			IsActive = true;
		}
		
		protected virtual void OnDisabled(){}
		internal virtual void OnDisable()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnDisable",this);
			if(!CanWork) return;
			
			FindView();
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			OnDisabled();
			ViewCore.CurrentId = tempId;
			
			IsActive = false;
		}
		
		protected virtual void OnDestroyed(){}
		internal virtual void OnDestroy()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnDestroy",this);
			if(!CanWork) return;
			
			FindView();
			string tempId = ViewCore.CurrentId;
			ViewCore.CurrentId = ViewId;
			OnDestroyed();
			ViewCore.CurrentId = tempId;
			
			IsActive = false;
		}
		#endregion
		
		
		#region Custom
		private bool isActive;
		public bool IsActive
		{
			get
			{
				return isActive;
			}
			set
			{
				if(!CanWork) return;
				if(isActive == value) return;
				isActive = value;
				if(CanDebug) Debug.Log($"{this.name}:IsActive:{isActive}",this);
				
				FindView();
				string tempId = ViewCore.CurrentId;
				ViewCore.CurrentId = ViewId;
				OnActive(value);
				ViewCore.CurrentId = tempId;
			}
		}
		protected virtual void OnActive(bool value){}
		#endregion
	}
}
#endif
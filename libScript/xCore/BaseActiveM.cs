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
			if(!CanWork) return;
			FindView();
			
			ViewIdApply();
			Awaked();
			ViewIdRestore();
		}
		
		private bool isStarted = false;
		protected virtual void Started(){}
		internal virtual void Start()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Start");
			if(!CanWork) return;
			FindView();
			
			ViewIdApply();
			Started();
			ViewIdRestore();
			
			isStarted = true;
			OnEnable();
		}
		
		protected virtual void OnEnabled(){}
		internal virtual void OnEnable()
		{
			if(!isStarted) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnEnable");
			if(!CanWork) return;
			FindView();
			
			ViewIdApply();
			OnEnabled();
			ViewIdRestore();
			
			IsActive = true;
		}
		
		protected virtual void OnDisabled(){}
		internal virtual void OnDisable()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisable");
			if(!CanWork) return;
			FindView();
			
			ViewIdApply();
			OnDisabled();
			ViewIdRestore();
			
			IsActive = false;
		}
		
		protected virtual void OnDestroyed(){}
		internal virtual void OnDestroy()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDestroy");
			if(!CanWork) return;
			FindView();
			
			ViewIdApply();
			OnDestroyed();
			ViewIdRestore();
			
			IsActive = false;
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
				FindView();
				
				ViewIdApply();
				OnActive(value);
				ViewIdRestore();
			}
		}
		protected virtual void OnActive(bool value){}
		#endregion
	}
}
#endif
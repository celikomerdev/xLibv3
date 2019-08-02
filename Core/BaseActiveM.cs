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
			
			ApplyViewIdWithDebug();
			Awaked();
			ApplyLastIdWithDebug();
		}
		
		private bool isStarted = false;
		protected virtual void Started(){}
		internal virtual void Start()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Start");
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
			if(!isStarted) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnEnable");
			if(!CanWork) return;
			FindView();
			
			ApplyViewIdWithDebug();
			OnEnabled();
			ApplyLastIdWithDebug();
			
			IsActive = true;
		}
		
		protected virtual void OnDisabled(){}
		internal virtual void OnDisable()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDisable");
			if(!CanWork) return;
			FindView();
			
			ApplyViewIdWithDebug();
			OnDisabled();
			ApplyLastIdWithDebug();
			
			IsActive = false;
		}
		
		protected virtual void OnDestroyed(){}
		internal virtual void OnDestroy()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDestroy");
			if(!CanWork) return;
			FindView();
			
			ApplyViewIdWithDebug();
			OnDestroyed();
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
				FindView();
				
				ApplyViewIdWithDebug();
				SetActive(value);
				ApplyLastIdWithDebug();
			}
		}
		protected virtual void SetActive(bool value){}
		#endregion
	}
}
#endif
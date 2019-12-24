#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class SingletonM<T> : BaseWorkM where T : Component
	{
		[Header("Singleton")]
		[SerializeField]private bool isPrimary = false;
		
		private static SingletonM<T> insBase = null;
		public static T ins = null;
		
		protected virtual void Awaked(){}
		protected virtual void Awake()
		{
			if(ins==this) return;
			TryReplace();
			if(!ins)
			{
				if(CanDebug) Debug.LogWarning($"{this.name}:Awaked",this);
				insBase = this;
				ins = this as T;
				Awaked();
			}
			else if(ins!=this)
			{
				DestroyImmediate();
			}
		}
		
		public void AwakeForced()
		{
			Awake();
		}
		
		private void TryReplace()
		{
			if(!ins) return;
			if(insBase.isPrimary && !isPrimary) return;
			insBase.DestroyImmediate();
			if(CanDebug) Debug.LogWarning($"{this.name}:Replaced",this);
		}
		
		protected virtual void Started(){}
		protected virtual void Start()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Started",this);
			Started();
		}
		
		public virtual void Init()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Init",this);
		}
		
		protected virtual void OnEnabled(){}
		protected virtual void OnEnable()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarning($"{this.name}:OnEnabled",this);
			OnEnabled();
		}
		
		protected virtual void OnDisabled(){}
		protected virtual void OnDisable()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarning($"{this.name}:OnDisabled",this);
			OnDisabled();
		}
		
		protected virtual void OnDestroyed(){}
		protected virtual void OnDestroy()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarning($"{this.name}:OnDestroyed",this);
			OnDestroyed();
		}
		
		private void DestroyImmediate()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:DestroyImmediate",this);
			DestroyImmediate(this.gameObject);
			if(ins==this) ins = null;
		}
	}
}
#endif
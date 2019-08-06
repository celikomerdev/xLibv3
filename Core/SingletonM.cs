#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class SingletonM<T> : BaseWorkM where T : Component
	{
		[Header("Singleton")]
		[SerializeField]private bool keepOld = false;
		
		private static SingletonM<T> insBase = null;
		public static T ins = null;
		
		protected virtual void Awaked(){}
		protected virtual void Awake()
		{
			if(ins==this) return;
			TryReplace();
			if(!ins)
			{
				if(CanDebug) Debug.LogWarningFormat(this,this.name+":Awaked");
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
			if(keepOld) return;
			if(!ins) return;
			insBase.DestroyImmediate();
			Debug.LogWarningFormat(this,this.name+":Replaced");
		}
		
		protected virtual void Started(){}
		protected virtual void Start()
		{
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":Started");
			Started();
		}
		
		public virtual void Init()
		{
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":Init");
		}
		
		protected virtual void OnEnabled(){}
		protected virtual void OnEnable()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":OnEnabled");
			OnEnabled();
		}
		
		protected virtual void OnDisabled(){}
		protected virtual void OnDisable()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":OnDisabled");
			OnDisabled();
		}
		
		protected virtual void OnDestroyed(){}
		protected virtual void OnDestroy()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":OnDestroyed");
			OnDestroyed();
		}
		
		private void DestroyImmediate()
		{
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":DestroyImmediate");
			DestroyImmediate(this.gameObject);
			if(ins==this) ins = null;
		}
	}
}
#endif
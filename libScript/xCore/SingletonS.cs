#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class SingletonS<T> : BaseWorkS where T : BaseWorkS
	{
		[Header("Singleton")]
		public static T ins;
		private void SetInstance()
		{
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":SetInstance");
			if(!ins)
			{
				if(CanDebug) Debug.LogWarningFormat(this,this.name+":Set");
				ins = this as T;
			}
			else if(ins!=this)
			{
				Debug.LogWarningFormat(this,this.name+":Skipped");
			}
		}
		
		protected virtual void Awaked(){}
		private void Awake()
		{
			SetInstance();
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":Awake");
			Awaked();
		}
		
		public void AwakeForced()
		{
			Awake();
		}
		
		private void OnEnable()
		{
			SetInstance();
		}
		
		private void OnDisable()
		{
			ins = null;
		}
		
		protected virtual void OnDestroyed(){}
		private void OnDestroy()
		{
			if(ins!=this) return;
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":OnDestroy");
			OnDestroyed();
			ins = null;
		}
	}
}
#endif
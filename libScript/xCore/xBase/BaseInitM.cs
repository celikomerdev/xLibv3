#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseInitM : BaseWorkM
	{
		protected bool isInit = false;
		public void Init(bool value)
		{
			if(isInit == value) return;
			isInit = value;
			#if CanTrace
			if(CanDebug) Debug.Log($"{this.name}:Init:{isInit}",this);
			#endif
			OnInit(isInit);
			SetDebug();
		}
		protected virtual void OnInit(bool init){}
		
		
		#region Flow
		protected BaseInitM()
		{
			MnThread.ScheduleLate(iDebug:this,call:Awake);
		}
		
		protected virtual void Awake()
		{
			// if(!CanWork) return;
			Init(true);
		}
		
		protected virtual void OnDestroy()
		{
			// if(!CanWork) return;
			Init(false);
		}
		
		[ContextMenu ("ReInit")]
		private void ReInit()
		{
			if(!CanWork) return;
			Init(!isInit);
			Init(!isInit);
		}
		#endregion
	}
}
#endif
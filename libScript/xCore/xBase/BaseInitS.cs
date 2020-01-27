#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseInitS : BaseWorkS
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
		// public BaseInitS()
		// {
		// 	MnThread.ScheduleLate(iDebug:this,call:delegate{OnEnable();});
		// }
		
		protected virtual void OnEnable()
		{
			if(CanWork) Init(true);
		}
		
		protected virtual void OnDisable()
		{
			if(CanWork) Init(false);
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
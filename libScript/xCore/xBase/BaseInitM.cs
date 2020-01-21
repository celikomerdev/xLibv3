﻿#if xLibv3
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
			if(CanDebug) Debug.LogFormat(this,this.name+":Init:{0}",isInit);
			OnInit(isInit);
			SetDebug();
		}
		protected virtual void OnInit(bool init){}
		
		
		#region Flow
		public BaseInitM()
		{
			MnThread.ScheduleLate(iDebug:this,call:delegate{Awake();});
		}
		
		protected virtual void Awake()
		{
			if(CanWork) Init(true);
		}
		
		protected virtual void OnDestroy()
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
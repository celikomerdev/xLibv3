﻿#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.EventClass;

namespace xLib.libAdvert
{
	public abstract class AdvertBase : BaseRegisterM
	{
		[Header("Key")]
		public string key = "";
		protected string idPlatform = "";
		
		[Header("Adapter")]
		protected string nameAdepter = "adapter_null";
		protected virtual void NameAdapter(){}
		private string NameAdapterBase
		{
			get
			{
				NameAdapter();
				if(string.IsNullOrEmpty(nameAdepter)) nameAdepter = "adapter_null";
				return nameAdepter;
			}
		}
		
		[Header("Analytics")]
		public bool isAnalytics = true;
		private Dictionary<string,object> Data
		{
			get
			{
				return new Dictionary<string,object>{{"unit",this.name},{"adapter",NameAdapterBase}};
			}
		}
		
		#region Register
		protected override bool OnRegister(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegister:{0}:{1}",key,value);
			return value;
		}
		
		protected void IdPlatform()
		{
			idPlatform = MnKey.GetValue(key);
			if(CanDebug) Debug.LogFormat(this,this.name+":IdPlatform:{0}",idPlatform);
		}
		
		protected void OnRegisterBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRegisterBase");
			isLoad = false;
			inLoad = false;
			if(baseRegister.onRegister) LoadBase();
		}
		#endregion
		
		
		#region Callback
		public EventBool onLoad;
		[NonSerialized]public bool isLoad;
		protected virtual void SetLoaded(bool value){}
		protected void SetLoadedBase(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":SetLoadedBase:{0}",value);
			
			inLoad = false;
			if(isLoad == value) return;
			isLoad = value;
			
			if(isLoad) StAnalytics.LogEvent(key:"advert_load_success",data:new Dictionary<string,object>{{"unit",this.name},{"adapter",NameAdapterBase}},canSend:isAnalytics);
			
			SetLoaded(value);
			onLoad.Invoke(isLoad);
		}
		
		public EventUnity onLoadFail;
		protected void OnLoadFailBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLoadFailBase");
			StAnalytics.LogEvent(key:"advert_load_fail",data:Data,canSend:isAnalytics);
			onLoadFail.Invoke();
			SetLoadedBase(false);
		}
		
		public EventUnity onShow;
		protected void OnShowBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnShowBase");
			StAnalytics.LogEvent(key:"advert_show",data:Data,canSend:isAnalytics);
			onShow.Invoke();
		}
		
		public EventInt onReward;
		protected void OnRewardBase(int value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRewardBase:{0}",value);
			if(value<1) return;
			StAnalytics.LogEvent(key:"advert_reward",data:Data,digit:value,canSend:isAnalytics);
			onReward.Invoke(value);
		}
		
		public EventUnity onClick;
		protected void OnClickBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnClickBase");
			StAnalytics.LogEvent(key:"advert_click",data:Data,canSend:isAnalytics);
			onClick.Invoke();
		}
		
		protected void OnVisitBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnVisitBase");
			StAnalytics.LogEvent(key:"advert_visit",data:Data,canSend:isAnalytics);
			// onVisit.Invoke();
		}
		
		public EventUnity onClose;
		protected void OnCloseBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCloseBase");
			StAnalytics.LogEvent(key:"advert_close",data:Data,canSend:isAnalytics);
			onClose.Invoke();
		}
		#endregion
		
		
		#region Public
		private bool inLoad;
		public void LoadBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LoadBase");
			if(!IsRegister) return;
			if(isLoad) return;
			if(inLoad) return;
			inLoad = true;
			Load();
		}
		protected virtual void Load()
		{
			SetLoadedBase(true);
		}
		
		public void ShowBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ShowBase");
			if(!isLoad) return;
			Show();
		}
		protected virtual void Show()
		{
			OnShowBase();
			MnCoroutine.ins.WaitForSeconds(delay:1.0f,call:delegate
			{
				OnRewardBase(1);
				OnCloseBase();
			});
		}
		#endregion
	}
}
#endif
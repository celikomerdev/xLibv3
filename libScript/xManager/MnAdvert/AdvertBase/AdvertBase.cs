#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib.libAdvert
{
	public abstract class AdvertBase : BaseRegisterM
	{
		[Header("Key")]
		public string key;
		protected string idPlatform;
		
		[Header("Adapter")]
		protected string nameAdepter = "AdapterNull";
		protected virtual void NameAdapter(){}
		private string NameAdapterBase
		{
			get
			{
				NameAdapter();
				if(string.IsNullOrEmpty(nameAdepter)) nameAdepter = "AdapterNull";
				return nameAdepter;
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
			
			if(isLoad) StAnalytics.LogEvent("Ads",this.name+":OnLoad",NameAdapterBase);
			
			SetLoaded(value);
			onLoad.Invoke(isLoad);
		}
		
		public EventUnity onLoadFail;
		protected void OnLoadFailBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLoadFailBase");
			StAnalytics.LogEvent("Ads",this.name+":OnLoadFail",NameAdapterBase);
			onLoadFail.Invoke();
			SetLoadedBase(false);
		}
		
		public EventUnity onShow;
		protected void OnShowBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnShowBase");
			StAnalytics.LogEvent("Ads",this.name+":OnShow",NameAdapterBase);
			onShow.Invoke();
		}
		
		public EventInt onReward;
		protected void OnRewardBase(int value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRewardBase:{0}",value);
			if(value<1) return;
			StAnalytics.LogEvent("Ads",this.name+":OnReward",NameAdapterBase,value.ToString());
			onReward.Invoke(value);
		}
		
		public EventUnity onClick;
		protected void OnClickBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnClickBase");
			StAnalytics.LogEvent("Ads",this.name+":OnClick",NameAdapterBase);
			onClick.Invoke();
		}
		
		protected void OnVisitBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnVisitBase");
			StAnalytics.LogEvent("Ads",this.name+":OnVisit",NameAdapterBase);
			// onVisit.Invoke();
		}
		
		public EventUnity onClose;
		protected void OnCloseBase()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnCloseBase");
			StAnalytics.LogEvent("Ads",this.name+":OnClose",NameAdapterBase);
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
			MnCoroutine.WaitForSeconds(delay:1.0f,call:delegate
			{
				OnRewardBase(1);
				OnCloseBase();
			});
		}
		#endregion
	}
}
#endif
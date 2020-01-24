#if xLibv3
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
		private Dictionary<string,object> Dict
		{
			get
			{
				return new Dictionary<string,object>{{"adapter",NameAdapterBase}};
			}
		}
		
		protected override void Awaked()
		{
			IdPlatform();
			base.Awaked();
		}
		
		#region TryRegister
		protected override bool TryRegister(bool register)
		{
			if(CanDebug) Debug.Log($"{this.name}:TryRegister:{key}:{register}",this);
			return register;
		}
		
		public string IdPlatform()
		{
			idPlatform = MnKey.GetValue(key);
			if(CanDebug) Debug.Log($"{this.name}:idPlatform:{idPlatform}",this);
			return idPlatform;
		}
		
		protected void OnRegisteredBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRegisteredBase",this);
			isLoad = false;
			inLoad = false;
			if(baseRegister.onRegister) LoadBase();
		}
		#endregion
		
		
		#region Callback
		public EventBool onLoad = new EventBool();
		protected bool isLoad = false;
		public bool IsLoad
		{
			get
			{
				return isLoad;
			}
		}
		protected virtual void SetLoaded(bool value){}
		protected void SetLoadedBase(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:SetLoadedBase:{value}",this);
			
			inLoad = false;
			if(isLoad == value) return;
			isLoad = value;
			
			if(isLoad) StAnalytics.LogEvent(group:"advert_load_success",key:this.name,dict:Dict,canSend:isAnalytics);
			
			SetLoaded(value);
			onLoad.Invoke(isLoad);
		}
		
		public EventUnity onLoadFail = new EventUnity();
		protected void OnLoadFailBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnLoadFailBase",this);
			StAnalytics.LogEvent(group:"advert_load_fail",key:this.name,dict:Dict,canSend:isAnalytics);
			onLoadFail.Invoke();
			SetLoadedBase(false);
		}
		
		public EventUnity onShow = new EventUnity();
		protected void OnShowBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnShowBase",this);
			StAnalytics.LogEvent(group:"advert_show",key:this.name,dict:Dict,canSend:isAnalytics);
			onShow.Invoke();
		}
		
		public EventInt onReward = new EventInt();
		protected void OnRewardBase(int reward)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRewardBase:reward:{reward}",this);
			StAnalytics.LogEvent(group:"advert_reward",key:this.name,digit:reward,dict:Dict,canSend:isAnalytics);
			onReward.Invoke(reward);
		}
		
		public EventUnity onClick = new EventUnity();
		protected void OnClickBase()
		{

			if(CanDebug) Debug.Log($"{this.name}:OnClickBase",this);
			StAnalytics.LogEvent(group:"advert_click",key:this.name,dict:Dict,canSend:isAnalytics);
			onClick.Invoke();
		}
		
		protected void OnVisitBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnVisitBase",this);
			StAnalytics.LogEvent(group:"advert_visit",key:this.name,dict:Dict,canSend:isAnalytics);
			// onVisit.Invoke();
		}
		
		public EventUnity onClose = new EventUnity();
		protected void OnCloseBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnCloseBase",this);
			StAnalytics.LogEvent(group:"advert_close",key:this.name,dict:Dict,canSend:isAnalytics);
			onClose.Invoke();
		}
		#endregion
		
		
		#region Public
		private bool inLoad = false;
		public void LoadBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadBase",this);
			if(!IsRegister) return;
			if(isLoad) return;
			if(inLoad) return;
			inLoad = true;
			StAnalytics.LogEvent(group:"advert_request",key:this.name,dict:Dict,canSend:isAnalytics);
			Load();
		}
		
		protected virtual void Load()
		{
			SetLoadedBase(true);
		}
		
		public void ShowBase()
		{
			if(CanDebug) Debug.Log($"{this.name}:ShowBase",this);
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
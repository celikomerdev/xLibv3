#if xLibv1
using System;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.libAdvert
{
	public class xAdvert : BaseRegisterM
	{
		public string key;
		public AdvertBase[] arrayAdvert;
		public byte[] arrayAdvertId;
		
		
		#region TryRegister
		protected override bool TryRegister(bool value)
		{
			mRegister(value);
			return value;
		}
		
		private void mRegister(bool value)
		{
			if(value)
			{
				for(int i = 0; i < arrayAdvert.Length; i++)
				{
					arrayAdvert[i].onShow.eventUnity.AddListener(OnShow);
					arrayAdvert[i].onClose.eventUnity.AddListener(OnClose);
					arrayAdvert[i].onReward.eventUnity.AddListener(OnReward);
				}
				
				currentOrder = 0;
				currentId = arrayAdvertId[currentOrder];
				Load();
			}
			else
			{
				for(int i = 0; i < arrayAdvert.Length; i++)
				{
					arrayAdvert[i].onShow.eventUnity.RemoveListener(OnShow);
					arrayAdvert[i].onClose.eventUnity.RemoveListener(OnClose);
					arrayAdvert[i].onReward.eventUnity.RemoveListener(OnReward);
				}
			}
		}
		#endregion
		
		
		#region Callback
		public UnityAction onShow = delegate(){};
		public void OnShow()
		{
			if(CanDebug) Debug.Log("OnShow: " + key);
			onShow.Invoke();
		}
		
		public UnityAction onClose = delegate(){};
		public void OnClose()
		{
			if(CanDebug) Debug.Log("OnClose: " + key);
			onClose.Invoke();
			
			Load();
			CurrentOrder++;
			Load();
		}
		
		public UnityAction onReward = delegate(){};
		public void OnReward()
		{
			if(CanDebug) Debug.Log("OnReward: " + key);
			onReward.Invoke();
		}
		#endregion
		
		
		#region Custom
		private int currentId;
		
		private int currentOrder;
		private int CurrentOrder
		{
			set
			{
				currentOrder = value%arrayAdvertId.Length;
				currentId = arrayAdvertId[currentOrder];
			}
			get
			{
				return currentOrder;
			}
		}
		
		private void Load()
		{
			if(!enabled) return;
			arrayAdvert[currentId].LoadBase();
		}
		
		public bool TryShow()
		{
			if(CanDebug) Debug.Log("TryShow");
			
			if(!enabled) return false;
			if(!gameObject.activeInHierarchy) return false;
			if(!IsLoadedAny()) return false;
			
			
			if(CanDebug) Debug.Log("Show: " + currentId);
			arrayAdvert[currentId].ShowBase();
			return true;
		}
		
		
		public bool IsLoaded(int id)
		{
			return (arrayAdvert[id].isLoad);
		}
		
		public bool IsLoadedAny()
		{
			bool returnValue = false;
			
			for(int i = 0; i < arrayAdvertId.Length; i++)
			{
				if(arrayAdvert[currentId].isLoad)
				{
					if(CanDebug) Debug.Log("Loaded: "+ arrayAdvert[currentId].key);
					returnValue = true;
					break;
				}
				
				if(CanDebug) Debug.Log("NotLoaded: "+ arrayAdvert[currentId].key);
				Load();
				CurrentOrder++;
			}
			
			if(CanDebug) Debug.Log("IsLoadedAny: "+ returnValue);
			return returnValue;
		}
		#endregion
	}
}
#endif
#if xLibv3
#if IapUnity
using System;
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribe : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool isSubscribed = new EventBool();
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.onInit.Listener(value,ListenResult,baseRegister.onRegister);
			MnProduct.ins.onRestore.Listener(value,ListenResult,baseRegister.onRegister);
			MnProduct.ins.onPurchase.Listener(value,ListenResult,baseRegister.onRegister);
			return value;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			
			Product product = null;
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			
			bool resultSubscribed = product.hasReceipt;
			
			if(!Application.isEditor)
			{
				SubscriptionInfo subscriptionInfo = HelperSubscription.GetInfo(product);
				if(subscriptionInfo == null)
				{
					resultSubscribed = false;
				}
				else
				{
					if(subscriptionInfo.isSubscribed() == Result.True) resultSubscribed = true;
					if(subscriptionInfo.isSubscribed() == Result.False) resultSubscribed = false;
					if(subscriptionInfo.isExpired() == Result.True) resultSubscribed = false;
					if(subscriptionInfo.getRemainingTime() <= TimeSpan.Zero) resultSubscribed = false;
				}
			}
			
			if(CanDebug) Debug.LogFormat(this,this.name+":isSubscribed:{0}",resultSubscribed);
			isSubscribed.Invoke(resultSubscribed);
		}
	}
}
#else
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribe : BaseRegisterM
	{
		[SerializeField]private string key;
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool isSubscribed = new EventBool();
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.isPurchase.Listener(value,ListenResult,true);
			return value;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			isSubscribed.Invoke(value);
		}
	}
}
#endif
#endif
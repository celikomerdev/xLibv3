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
		
		protected override bool TryRegister(bool register)
		{
			MnProduct.ins.isInit.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:false);
			MnProduct.ins.onRestore.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:false);
			MnProduct.ins.onPurchase.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:false);
			return register;
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
			
			if(CanDebug) Debug.Log($"{this.name}:isSubscribed:{resultSubscribed}",this);
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
		
		protected override bool TryRegister(bool register)
		{
			MnProduct.ins.onPurchase.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:true);
			return register;
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
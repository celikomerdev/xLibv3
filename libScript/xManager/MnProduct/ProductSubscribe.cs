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
		[SerializeField]private EventBool eventBool = new EventBool();
		
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.isInit.Listener(value,ListenResult,true);
			MnProduct.ins.isRestore.Listener(value,ListenResult,true);
			MnProduct.ins.isPurchase.Listener(value,ListenResult,true);
			return value;
		}
		
		private Product product = null;
		private void ListenResult(bool value)
		{
			if(!value) return;
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			
			bool result = product.hasReceipt;
			
			SubscriptionInfo subscriptionInfo = HelperSubscription.GetInfo(product);
			if(subscriptionInfo == null)
			{
				result = false;
			}
			else
			{
				if(subscriptionInfo.isExpired() == Result.True) result = false;
				if(subscriptionInfo.getRemainingTime() <= TimeSpan.Zero) result = false;
			}
			
			eventBool.Invoke(result);
		}
	}
}
#else
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribe : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[SerializeField]private EventBool eventBool = new EventBool();
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.isPurchase.Listener(value,ListenResult,true);
			return value;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			eventBool.Invoke(value);
		}
	}
}
#endif
#endif
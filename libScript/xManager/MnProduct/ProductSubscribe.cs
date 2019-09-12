﻿#if xLibv3
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
		[SerializeField]private EventBool eventSubscribe = new EventBool();
		
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.onInit.Listener(value,ListenResult,true);
			MnProduct.ins.onRestore.Listener(value,ListenResult,true);
			MnProduct.ins.onPurchase.Listener(value,ListenResult,true);
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
			
			eventSubscribe.Invoke(result);
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
		[SerializeField]private EventBool eventSubscribe = new EventBool();
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.isPurchase.Listener(value,ListenResult,true);
			return value;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			eventSubscribe.Invoke(value);
		}
	}
}
#endif
#endif
﻿#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribeInfo : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[SerializeField]public EventString eventStringFreeTrialPeriod = new EventString();
		
		protected override bool OnRegister(bool register)
		{
			MnProduct.ins.isInit.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:true);
			return register;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			// if(Application.isEditor) return;
			
			Product product = null;
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			
			string productDetails = (MnProduct.productsDetails == null || !MnProduct.productsDetails.ContainsKey(product.definition.storeSpecificId))? null:MnProduct.productsDetails[product.definition.storeSpecificId];
			if(productDetails==null)
			{
				Debug.LogException(new UnityException($"{key}:productDetails:null"),this);
				return;
			}
			SubscriptionManager subscriptionManager = new SubscriptionManager(product,productDetails);
			if(subscriptionManager == null)
			{
				Debug.LogException(new UnityException($"{key}:subscriptionInfo:null"),this);
				return;
			}
			SubscriptionInfo subscriptionInfo = subscriptionManager.getSubscriptionInfo();
			if(subscriptionInfo == null)
			{
				Debug.LogException(new UnityException($"{key}:subscriptionInfo:null"),this);
				return;
			}
			
			if(CanDebug) Debug.Log($"{key}:subscriptionInfo:{subscriptionInfo.ToString()}",this);
			eventStringFreeTrialPeriod.Value = subscriptionInfo.getFreeTrialPeriodString();
		}
	}
}
#else
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribeInfo : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[SerializeField]public EventString eventStringFreeTrialPeriod = new EventString();
		
		protected override bool OnRegister(bool register)
		{
			eventStringFreeTrialPeriod.Value = "1 Week Free Try";
			return register;
		}
	}
}
#endif
#endif
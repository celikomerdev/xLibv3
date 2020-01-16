#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribeInfo : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[SerializeField]public EventBool eventFreeTrialAvaible = new EventBool();
		[SerializeField]public EventTimeSpan eventFreeTrialPeriod = new EventTimeSpan();
		
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
			
			bool freeTrialAvaible = true;
			if(subscriptionInfo.isFreeTrial() != Result.True) freeTrialAvaible = false;
			if(subscriptionInfo.isExpired() == Result.True) freeTrialAvaible = false;
			
			eventFreeTrialAvaible.Value = freeTrialAvaible;
			eventFreeTrialPeriod.Value = subscriptionInfo.getFreeTrialPeriod();
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
		[SerializeField]public EventBool eventFreeTrialAvaible = new EventBool();
		[SerializeField]public EventTimeSpan eventFreeTrialPeriod = new EventTimeSpan();
		
		protected override bool OnRegister(bool register)
		{
			eventFreeTrialAvaible.Value = true;
			eventFreeTrialPeriod.Value = System.TimeSpan.FromDays(7);
			return register;
		}
	}
}
#endif
#endif
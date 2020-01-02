#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.Purchasing;
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
			
			Product product = null;
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			
			SubscriptionInfo subscriptionInfo = HelperSubscription.GetInfo(product);
			if(subscriptionInfo != null)
			{
				if(CanDebug) Debug.Log($"{this.name}:subscriptionInfo:{subscriptionInfo.ToString()}",this);
				eventStringFreeTrialPeriod.Value = subscriptionInfo.getFreeTrialPeriodString();
			}
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
		[SerializeField]private string key = "";
		[SerializeField]public EventString eventStringFreeTrialPeriod = new EventString();
		
		protected override bool OnRegister(bool register)
		{
			MnProduct.ins.onPurchase.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:true);
			return register;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			eventStringFreeTrialPeriod.Value = "Free Trial Period";
		}
	}
}
#endif
#endif
#if xLibv3
#if IapUnity
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductSubscribeInfo : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[SerializeField]public EventTimeSpan eventFreeTrialPeriod = new EventTimeSpan();
		
		protected override bool TryRegister(bool register)
		{
			MnProduct.ins.isInit.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:baseRegister.onRegister);
			return register;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			
			Product product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			
			eventFreeTrialPeriod.Value = FreeTrialPeriod(product);
		}
		
		private TimeSpan FreeTrialPeriod(Product product)
		{
			TimeSpan returnValue = TimeSpan.FromTicks(0);
			if(product == null) return returnValue;
			
			if(MnProduct.productsDetails == null)
			{
				if(CanDebug) Debug.LogWarning($"{key}:productsDetails:null",this);
				return returnValue;
			}
			
			if(!MnProduct.productsDetails.ContainsKey(product.definition.storeSpecificId))
			{
				if(CanDebug) Debug.LogWarning($"{key}:!ContainsKey:{product.definition.storeSpecificId}",this);
				return returnValue;
			}
			
			string productDetailsString = MnProduct.productsDetails[product.definition.storeSpecificId];
			if(CanDebug) Debug.Log($"{key}:productDetailsString:{productDetailsString}",this);
			JObject productDetails = JObject.Parse(productDetailsString);
			
			string stringFreeTrial = productDetails.GetTokenSafe<string>("freeTrialPeriod","");
			if(CanDebug) Debug.Log($"{key}:stringFreeTrial:{stringFreeTrial}",this);
			
			returnValue = System.Xml.XmlConvert.ToTimeSpan(stringFreeTrial);
			return returnValue;
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
		[SerializeField]public EventTimeSpan eventFreeTrialPeriod = new EventTimeSpan();
		
		protected override bool TryRegister(bool register)
		{
			eventFreeTrialPeriod.Value = TimeSpan.FromDays(1);
			return register;
		}
	}
}
#endif
#endif
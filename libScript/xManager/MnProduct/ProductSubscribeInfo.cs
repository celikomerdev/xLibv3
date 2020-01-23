#if xLibv3
#if IapUnity
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
			MnProduct.ins.isInit.Listener(register:register,call:ListenResult,viewId:ViewId,order:baseRegister.order,onRegister:true);
			return register;
		}
		
		private void ListenResult(bool value)
		{
			if(!value) return;
			if(Application.isEditor)
			{
				eventFreeTrialPeriod.Value = System.Xml.XmlConvert.ToTimeSpan("P3D");
				return;
			}
			
			Product product = null;
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(product.definition.type != ProductType.Subscription) return;
			if(MnProduct.productsDetails == null) return;
			if(!MnProduct.productsDetails.ContainsKey(product.definition.storeSpecificId)) return;
			
			string productDetailsString = MnProduct.productsDetails[product.definition.storeSpecificId];
			if(CanDebug) Debug.Log($"{key}:productDetailsString:{productDetailsString}",this);
			JObject productDetails = JObject.Parse(productDetailsString);
			
			string stringFreeTrial = productDetails.GetTokenSafe<string>("freeTrialPeriod","");
			eventFreeTrialPeriod.Value = System.Xml.XmlConvert.ToTimeSpan(stringFreeTrial);
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
			eventFreeTrialPeriod.Value = System.Xml.XmlConvert.ToTimeSpan("P3D");
			return register;
		}
	}
}
#endif
#endif
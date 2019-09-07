#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductRestore : BaseRegisterM
	{
		[SerializeField]private string key = "";
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventRestore = new EventBool();
		
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
			if(product.definition.type != ProductType.NonConsumable) return;
			
			eventRestore.Invoke(product.hasReceipt);
		}
	}
}
#else
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductRestore : BaseRegisterM
	{
		[SerializeField]private string key;
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventRestore = new EventBool();
		
		protected override bool OnRegister(bool value)
		{
			MnProduct.ins.isPurchase.Listener(value,ListenResult,true);
			return value;
		}
		
		private void ListenResult(bool value)
		{
			eventRestore.Invoke(value);
		}
	}
}
#endif
#endif
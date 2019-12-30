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
		[SerializeField]private bool restoreOnce = false;
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventRestore = new EventBool();
		
		protected override bool OnRegister(bool register)
		{
			if(!restoreOnce) MnProduct.ins.onInit.Listener(register:register, call:ListenRestore, viewId:ViewId, order:baseRegister.order, onRegister:true);
			MnProduct.ins.onRestore.Listener(register:register, call:ListenRestore, viewId:ViewId, order:baseRegister.order, onRegister:true);
			MnProduct.ins.onPurchase.Listener(register:register, call:ListenPurchase, viewId:ViewId, order:baseRegister.order, onRegister:true);
			return register;
		}
		
		private void ListenRestore(bool value)
		{
			if(restoreOnce && MnProduct.ins.isRestore.Value) return;
			ListenPurchase(value);
		}
		
		private Product product = null;
		private void ListenPurchase(bool value)
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
		[SerializeField]private string key = "";
		[SerializeField]private bool restoreOnce = false;
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventRestore = new EventBool();
		
		protected override bool OnRegister(bool register)
		{
			MnProduct.ins.onRestore.Listener(register:register, call:ListenRestore, viewId:ViewId, order:baseRegister.order, onRegister:true);
			MnProduct.ins.onPurchase.Listener(register:register, call:ListenPurchase, viewId:ViewId, order:baseRegister.order, onRegister:true);
			return register;
		}
		
		private void ListenRestore(bool value)
		{
			if(restoreOnce && MnProduct.ins.isRestore.Value) return;
			ListenPurchase(value);
		}
		
		private void ListenPurchase(bool value)
		{
			if(!value) return;
			if(MnProduct.currentProductId != key) return;
			eventRestore.Invoke(value);
		}
	}
}
#endif
#endif
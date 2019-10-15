#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductPurchase : BaseActiveM
	{
		private Product product = null;
		[SerializeField]private string key = "";
		[SerializeField]public EventString eventPrice = new EventString();
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.onInit.Listener(true,ListenInit,true);
		}
		
		protected override void OnEnabled()
		{
			RefreshProduct();
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.onInit.Listener(false,ListenInit);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.onInit.Listener(false,ListenInit);
			RefreshProduct();
		}
		
		private void RefreshProduct()
		{
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = product.metadata.localizedPriceString;
		}
		#endregion
		
		#region Register
		private bool isRegister;
		private void Register(bool value)
		{
			if(isRegister == value) return;
			isRegister = value;
			MnProduct.ins.onPurchase.Listener(value,IsPuchase);
		}
		#endregion
		
		#region Fuction
		public void Purchase()
		{
			if(Application.internetReachability == NetworkReachability.NotReachable) return;
			RefreshProduct();
			
			if(product == null) return;
			Register(true);
			MnProduct.ins.Purchase(product);
		}
		
		private void IsPuchase(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsPuchase:{0}",value);
			if(product != MnProduct.currentProduct)
			{
				xDebug.LogExceptionFormat(this,"product id does not match:{0}",product);
				return;
			}
			
			Register(false);
			eventPurchase.Invoke(value);
		}
		#endregion
	}
}
#else
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductPurchase : BaseActiveM
	{
		private string product = null;
		[SerializeField]private string key = "";
		[SerializeField]public EventString eventPrice = new EventString();
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.onInit.Listener(true,ListenInit,viewId:ViewId,onRegister:true);
		}
		
		protected override void OnEnabled()
		{
			RefreshProduct();
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.onInit.Listener(false,ListenInit,viewId:ViewId);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.onInit.Listener(false,ListenInit,viewId:ViewId);
			RefreshProduct();
		}
		
		private void RefreshProduct()
		{
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = "0.00$";
		}
		#endregion
		
		#region Register
		private bool isRegister;
		private void Register(bool register)
		{
			if(isRegister == register) return;
			isRegister = register;
			MnProduct.ins.onPurchase.Listener(register,IsPuchase,viewId:ViewId);
		}
		#endregion
		
		#region Fuction
		public void Purchase()
		{
			if(Application.internetReachability == NetworkReachability.NotReachable) return;
			RefreshProduct();
			
			if(product == null) return;
			Register(true);
			MnProduct.ins.Purchase(product);
		}
		
		private void IsPuchase(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsPuchase:{0}",value);
			if(product != MnProduct.currentProductId)
			{
				xDebug.LogExceptionFormat(this,"product id does not match:{0}",product);
				return;
			}
			
			Register(false);
			eventPurchase.Invoke(value);
		}
		#endregion
	}
}
#endif
#endif
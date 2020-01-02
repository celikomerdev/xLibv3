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
		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
				ClearProduct();
				RefreshProduct();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventPrice")]
		[SerializeField]public EventString eventPriceString = new EventString();
		
		[SerializeField]public EventFloat eventPrice = new EventFloat();
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.isInit.Listener(register:true,call:ListenInit,onRegister:true);
		}
		
		protected override void OnEnabled()
		{
			RefreshProduct();
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.isInit.Listener(register:false,call:ListenInit);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.isInit.Listener(register:false,call:ListenInit);
			RefreshProduct();
		}
		
		private void ClearProduct()
		{
			product = null;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "1.00$";
		}
		
		private void RefreshProduct()
		{
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = (float)product.metadata.localizedPrice;
			eventPriceString.Value = product.metadata.localizedPriceString;
		}
		#endregion
		
		#region Register
		private bool isRegister = false;
		private void Register(bool value)
		{
			if(isRegister == value) return;
			isRegister = value;
			MnProduct.ins.onPurchase.Listener(register:value,call:IsPuchase);
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
			if(CanDebug) Debug.Log($"{this.name}:IsPuchase:{value}",this);
			if(product != MnProduct.currentProduct)
			{
				xLogger.LogException($"product id does not match:{product}",this);
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
		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
				ClearProduct();
				RefreshProduct();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventPrice")]
		[SerializeField]public EventString eventPriceString = new EventString();
		
		[SerializeField]public EventFloat eventPrice = new EventFloat();
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.onInit.Listener(register:true,call:ListenInit,onRegister:true);
		}
		
		protected override void OnEnabled()
		{
			RefreshProduct();
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.onInit.Listener(register:false,call:ListenInit);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.onInit.Listener(register:false,call:ListenInit);
			RefreshProduct();
		}
		
		private void ClearProduct()
		{
			product = null;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "1.00$";
		}
		
		private void RefreshProduct()
		{
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "1.00$";
		}
		#endregion
		
		#region Register
		private bool isRegister = false;
		private void Register(bool register)
		{
			if(isRegister == register) return;
			isRegister = register;
			MnProduct.ins.onPurchase.Listener(register:register,call:IsPuchase);
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
			if(CanDebug) Debug.Log($"{this.name}:IsPuchase:{value}",this);
			if(product != MnProduct.currentProductId)
			{
				xDebug.LogException($"product id does not match:{product}",this);
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
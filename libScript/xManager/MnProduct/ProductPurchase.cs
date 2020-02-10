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
				RefreshProduct();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventPrice")]
		[SerializeField]public EventString eventPriceString = new EventString();
		
		[SerializeField]public EventFloat eventPrice = new EventFloat();
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void OnEnabled()
		{
			MnProduct.ins.Init();
			MnProduct.ins.isInit.Listener(register:true,call:ListenInit,onRegister:true);
		}
		
		protected override void OnDisabled()
		{
			MnProduct.ins.isInit.Listener(register:false,call:ListenInit,onRegister:true);
		}
		
		private void ListenInit(bool value)
		{
			if(value) RefreshProduct();
		}
		
		private void ClearProduct()
		{
			product = null;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "$1.00";
		}
		
		public void RefreshProduct()
		{
			ClearProduct();
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = (float)product.metadata.localizedPrice;
			eventPriceString.Value = product.metadata.localizedPriceString;
		}
		#endregion
		
		#region ListenPurchase
		private bool isListenPurchase = false;
		private void ListenPurchase(bool value)
		{
			if(isListenPurchase == value) return;
			isListenPurchase = value;
			MnProduct.ins.onPurchase.Listener(register:value,call:IsPuchase);
		}
		#endregion
		
		#region Fuction
		public void Purchase()
		{
			MnProduct.ins.Init();
			if(Application.internetReachability == NetworkReachability.NotReachable)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Please Check Your Connection"));
				Debug.LogException(new UnityException($"Purchase:internetReachability:NotReachable"),this);
				return;
			}
			RefreshProduct();
			
			if(product == null) return;
			ListenPurchase(true);
			MnProduct.ins.Purchase(product);
		}
		
		private void IsPuchase(bool value)
		{
			ListenPurchase(false);
			if(CanDebug) Debug.Log($"{this.name}:IsPuchase:{value}",this);
			if(!Equals(product,MnProduct.currentProduct))
			{
				Debug.LogException(new UnityException($"product id does not match:{product}"),this);
				return;
			}
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
				RefreshProduct();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventPrice")]
		[SerializeField]public EventString eventPriceString = new EventString();
		
		[SerializeField]public EventFloat eventPrice = new EventFloat();
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]public EventBool eventPurchase = new EventBool();
		
		#region Behavior
		protected override void OnEnabled()
		{
			MnProduct.ins.isInit.Listener(register:true,call:ListenInit,onRegister:true);
		}
		
		protected override void OnDisabled()
		{
			MnProduct.ins.isInit.Listener(register:false,call:ListenInit,onRegister:true);
		}
		
		private void ListenInit(bool value)
		{
			if(value) RefreshProduct();
		}
		
		private void ClearProduct()
		{
			product = null;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "$1.00";
		}
		
		public void RefreshProduct()
		{
			ClearProduct();
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			eventPrice.Value = 1.00f;
			eventPriceString.Value = "$1.00";
		}
		#endregion
		
		#region ListenPurchase
		private bool isListenPurchase = false;
		private void ListenPurchase(bool register)
		{
			if(isListenPurchase == register) return;
			isListenPurchase = register;
			MnProduct.ins.onPurchase.Listener(register:register,call:IsPuchase);
		}
		#endregion
		
		#region Fuction
		public void Purchase()
		{
			if(Application.internetReachability == NetworkReachability.NotReachable)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Please Check Your Connection"));
				Debug.LogException(new UnityException($"Purchase:internetReachability:NotReachable"),this);
				return;
			}
			
			RefreshProduct();
			
			if(product == null) return;
			ListenPurchase(true);
			MnProduct.ins.Purchase(product);
		}
		
		private void IsPuchase(bool value)
		{
			ListenPurchase(false);
			if(CanDebug) Debug.Log($"{this.name}:IsPuchase:{value}",this);
			if(product != MnProduct.currentProductId)
			{
				Debug.LogException(new UnityException($"product id does not match:{product}"),this);
				return;
			}
			eventPurchase.Invoke(value);
		}
		#endregion
	}
}
#endif
#endif
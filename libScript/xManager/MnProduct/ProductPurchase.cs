#if xLibv3
using UnityEngine;
using UnityEngine.UI;
using xLib.EventClass;

namespace xLib.ToolPurchase
{
	public class ProductPurchase : BaseActiveM
	{
		[SerializeField]private string key = "";
		[SerializeField]private Text textPrice = null;
		[SerializeField]public EventBool eventBool = new EventBool();
		
		#if IapUnity
		private UnityEngine.Purchasing.Product product;
		#else
		private string product;
		#endif
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.isInit.Listener(true,ListenInit,true);
		}
		
		protected override void OnEnabled()
		{
			RefreshProduct();
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.isInit.Listener(false,ListenInit);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.isInit.Listener(false,ListenInit);
			RefreshProduct();
		}
		
		private void RefreshProduct()
		{
			if(product == null) product = MnProduct.ins.GetProduct(key);
			if(product == null) return;
			if(textPrice) textPrice.text = product.metadata.localizedPriceString;
		}
		#endregion
		
		#region Register
		private bool isRegister;
		private void Register(bool value)
		{
			if(isRegister == value) return;
			isRegister = value;
			MnProduct.ins.isPurchase.Listener(value,IsPuchase);
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
			eventBool.Invoke(value);
		}
		#endregion
	}
}
#endif
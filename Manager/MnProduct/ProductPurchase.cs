#if xLibv2
#if IapUnity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using xLib.ToolEventClass;

namespace xLib.ToolPurchase
{
	public class ProductPurchase : BaseActiveM
	{
		private Product product;
		[SerializeField]private string key;
		[SerializeField]private Text textPrice;
		[SerializeField]public EventBool eventBool;
		
		#region Behavior
		protected override void Started()
		{
			MnProduct.ins.isInit.Listener(true,ListenInit,true);
		}
		
		protected override void OnDestroyed()
		{
			MnProduct.ins.isInit.Listener(false,ListenInit);
		}
		
		private void ListenInit(bool value)
		{
			if(!value) return;
			MnProduct.ins.isInit.Listener(false,ListenInit);
			
			product = MnProduct.ins.GetProduct(key);
			if(textPrice) textPrice.text = product.metadata.localizedPriceString;
		}
		
		protected override void OnEnabled()
		{
			if(product==null) return;
			if(textPrice) textPrice.text = product.metadata.localizedPriceString;
		}
		#endregion
		
		#region Fuction
		public void Purchase()
		{
			if(Application.internetReachability == NetworkReachability.NotReachable) return;
			if(product==null) return;
			Register(true);
			MnProduct.ins.Purchase(product);
		}
		#endregion
		
		#region Register
		private bool isRegister;
		private void Register(bool value)
		{
			if(isRegister == value) return;
			isRegister = value;
			
			if(value)
			{
				StPopupBar.MessageLocalized("please wait");
			}
			
			MnProduct.ins.isPurchase.Listener(value,IsPuchase);
		}
		#endregion
		
		#region Callback
		private void IsPuchase(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsPuchase:{0}",value);
			if(product!=MnProduct.currentProduct) return;
			Register(false);
			
			
			if(!value)
			{
				StPopupBar.MessageLocalized("purchase failed");
			}
			else
			{
				StPopupBar.MessageLocalized("purchase successful");
			}
			
			
			eventBool.Invoke(value);
		}
		#endregion
	}
}
#else
using UnityEngine;
using UnityEngine.UI;
using xLib.ToolEventClass;

namespace xLib.ToolPurchase
{
	public class ProductPurchase : BaseM
	{
		[SerializeField]private string key;
		[SerializeField]private Text textPrice;
		[SerializeField]public EventBool eventBool;
		
		public void Purchase()
		{
			StPopupBar.MessageLocalized("purchase successful");
			MnProduct.ins.isPurchase.Value = true;
			eventBool.Invoke(true);
		}
	}
}
#endif
#endif
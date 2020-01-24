#if xLibv3
#if IapUnity
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.Purchasing;
using xLib.Purchasing.Security;
using xLib.xNode.NodeObject;


namespace xLib
{
	public class MnProduct : SingletonM<MnProduct>, IStoreListener
	{
		[SerializeField]private bool useValidate = true;
		[SerializeField]private FakeStoreUIMode fakeStoreUIMode = FakeStoreUIMode.StandardUser;
		
		#region Init
		private bool inInit = false;
		public NodeBool isInit = null;
		protected override void Inited()
		{
			if(isInit.Value) return;
			if(inInit) return;
			inInit = true;
			
			StandardPurchasingModule.Instance().useFakeStoreUIMode = fakeStoreUIMode;
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			
			ProductCatalog catalog = ProductCatalog.LoadDefaultCatalog();
			HelperCatalog.PopulateConfigurationBuilder(ref builder,catalog);
			
			HelperSubscription.CanDebug = CanDebug;
			ProductValidator.CanDebug = CanDebug;
			ProductValidator.UseValidate = useValidate;
			ProductValidator.Init();
			
			MnThread.StartThread(iDebug:this,useThread:false,priority:1,call:delegate{UnityPurchasing.Initialize(this,builder);});
		}
		
		#endregion
		
		
		#region Implementation
		private static IStoreController m_Controller = null;
		private static IExtensionProvider m_extensions = null;
		
		public static Dictionary<string, string> productsDetails = new Dictionary<string,string>();
		private static IGooglePlayStoreExtensions m_GooglePlayStoreExtensions = null;
		private static IAppleExtensions m_AppleExtensions = null;
		
		void IStoreListener.OnInitialized(IStoreController controller,IExtensionProvider extensions)
		{
			m_Controller = controller;
			m_extensions = extensions;
			
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
			{
				m_GooglePlayStoreExtensions = m_extensions.GetExtension<IGooglePlayStoreExtensions>();
				productsDetails = m_GooglePlayStoreExtensions.GetProductJSONDictionary();
			}
			else if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore)
			{
				m_AppleExtensions = m_extensions.GetExtension<IAppleExtensions>();
				// productDetails = m_AppleExtensions.GetIntroductoryPriceDictionary();
				productsDetails = m_AppleExtensions.GetProductDetails();
			}
			
			if(CanDebug)
			{
				foreach (var item in controller.products.all)
				{
					Debug.Log(string.Join("-",
					new[]
					{
						item.definition.id,
						item.definition.storeSpecificId,
						item.definition.type.ToString(),
						item.metadata.localizedPrice.ToString(),
						item.metadata.localizedPriceString
					}));
				}
				
				if(productsDetails==null)
				{
					Debug.LogWarning($"{this.name}:productsDetails:null",this);
				}
				else
				{
					foreach (KeyValuePair<string,string> productDetails in productsDetails)
					{
						Debug.Log($"{this.name}:productsDetails:{productDetails.Key}:{productDetails.Value}",this);
					}
				}
			}
			
			OnInit(true);
		}
		
		void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
		{
			Debug.LogException(new UnityException($"{this.name}:OnInitializeFailed:{error}"),this);
			OnInit(false);
		}
		
		PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
		{
			currentProduct = args.purchasedProduct;
			bool isValid = ProductValidator.IsValid(args);
			
			if(CanDebug) Debug.Log($"{this.name}:OnPurchaseProcess:{isValid}:{args.purchasedProduct.definition.id}",this);
			StAnalytics.LogEvent(group:"iap_purchase_process_"+isValid,key:args.purchasedProduct.definition.id);
			
			OnPurchase(isValid,args.purchasedProduct.definition.id);
			
			return PurchaseProcessingResult.Complete;
		}
		
		void IStoreListener.OnPurchaseFailed(Product product,PurchaseFailureReason productFailureReason)
		{
			currentProduct = product;
			
			Debug.LogException(new UnityException($"{this.name}:OnPurchaseFailed:{productFailureReason}:{product.definition.id}"),this);
			StAnalytics.LogEvent(group:"iap_purchase_fail_"+productFailureReason,key:product.definition.id);
			
			OnPurchase(false,product.definition.id);
		}
		#endregion
		
		
		#region Fuctions
		#region Purchase
		public NodeBool inPurchase = null;
		public void Purchase(Product product)
		{
			currentProduct = product;
			
			if(product == null) return;
			if(inPurchase.Value) return;
			inPurchase.Value = true;
			if(CanDebug) Debug.Log($"{this.name}:Purchase:{product.definition.id}",this);
			
			StPopupBar.QueueMessage(MnLocalize.GetValue("Please Wait"));
			m_Controller.InitiatePurchase(product);
		}
		
		public void Purchase(string key)
		{
			Product temp = GetProduct(key);
			
			if(temp == null)
			{
				OnPurchase(false,key);
				return;
			}
			
			Purchase(temp);
		}
		#endregion
		
		#region Restore
		public NodeBool inRestore = null;
		public void Restore()
		{
			if(inRestore.Value) return;
			inRestore.Value = true;
			if(CanDebug) Debug.Log($"{this.name}:Restore",this);
			
			if(!isInit.Value)
			{
				Init();
				OnRestoreFalse();
				return;
			}
			
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
			{
				OnRestoreTrue(false);
			}
			else if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore)
			{
				m_AppleExtensions.RestoreTransactions(OnRestoreTrue);
			}
			else
			{
				if(CanDebug) Debug.LogWarning($"{this.name}:NotSupported:{StandardPurchasingModule.Instance().appStore}",this);
				OnRestoreFalse();
			}
		}
		#endregion
		#endregion
		
		
		#region Callback
		public static Product currentProduct = null;
		public static string currentProductId
		{
			get{return currentProduct.definition.id;}
		}
		
		public Product GetProduct(string key)
		{
			if(isInit.Value) return m_Controller.products.WithID(key);
			else return null;
		}
		
		private void OnInit(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnInit:{value}",this);
			inInit = false;
			isInit.Value = value;
			if(!value) return;
		}
		
		public NodeBool onPurchase = null;
		public static Action<bool,string> onPurchaseProduct = delegate{};
		private void OnPurchase(bool result,string productId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnPurchase:{result}:{productId}",this);
			
			if(!result) StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Failed"));
			else StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Successful"));
			
			onPurchase.Value = result;
			onPurchaseProduct(result,productId);
			
			inPurchase.Value = false;
		}
		
		public NodeBool onRestore = null;
		private void OnRestoreTrue(bool isContinue)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRestore:True:isContinue:{isContinue}",this);
			StPopupBar.QueueMessage(MnLocalize.GetValue("Restore Successful"));
			inRestore.Value = false;
			onRestore.Value = true;
		}
		
		private void OnRestoreFalse()
		{
			if(CanDebug) Debug.Log($"{this.name}:OnRestore:False",this);
			StPopupBar.QueueMessage(MnLocalize.GetValue("Restore Failed"));
			inRestore.Value = false;
			onRestore.Value = false;
		}
		#endregion
	}
}
#else
using System;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnProduct : SingletonM<MnProduct>
	{
		#pragma warning disable
		[SerializeField]private bool useValidate = true;
		[SerializeField]private int fakeStoreUIMode = 1;
		
		public NodeBool isInit = null;
		protected override void Inited()
		{
			isInit.Value = true;
		}
		
		public NodeBool inPurchase = null;
		public void Purchase(string productId)
		{
			if(CanDebug) Debug.Log($"{this.name}:Purchase:{productId}",this);
			currentProductId = productId;
			
			if(string.IsNullOrWhiteSpace(productId)) return;
			if(inPurchase.Value) return;
			inPurchase.Value = true;
			
			StPopupBar.QueueMessage(MnLocalize.GetValue("Please Wait"));
			
			StPopupWindow.Reset();
			StPopupWindow.HeaderLocalized("warning");
			StPopupWindow.Body(string.Format($"{productId}\n$1.00\nbuy?"));
			StPopupWindow.AcceptLocalized("yes");
			StPopupWindow.DeclineLocalized("no");
			StPopupWindow.Listener(true,Listener);
			void Listener(bool result)
			{
				StPopupWindow.Listener(false,Listener);
				OnPurchase(result,productId);
			}
			StPopupWindow.Show();
		}
		
		public NodeBool onPurchase = null;
		public static Action<bool,string> onPurchaseProduct = delegate{};
		private void OnPurchase(bool result,string productId)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnPurchase:{result}:{productId}",this);
			currentProductId = productId;
			
			if(!result) StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Failed"));
			else StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Successful"));
			
			onPurchase.Value = result;
			onPurchaseProduct(result,productId);
			
			inPurchase.Value = false;
		}
		
		public NodeBool inRestore = null;
		public NodeBool onRestore = null;
		public void Restore()
		{
			if(inRestore.Value) return;
			inRestore.Value = true;
			
			if(CanDebug) Debug.Log($"{this.name}:Restore",this);
			StPopupBar.QueueMessage(MnLocalize.GetValue("Restore Successful"));
			
			inRestore.Value = false;
			onRestore.Value = true;
		}
		
		public static string currentProductId = null;
		public string GetProduct(string key)
		{
			if(isInit.Value) return key;
			else return null;
		}
		#pragma warning restore
	}
}
#endif
#endif
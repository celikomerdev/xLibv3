#if xLibv3
#if IapUnity
using System;
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
		private bool inInit;
		public override void Init()
		{
			base.Init();
			if(onInit.Value) return;
			if(inInit) return;
			inInit = true;
			
			StandardPurchasingModule.Instance().useFakeStoreUIMode = fakeStoreUIMode;
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			
			ProductCatalog catalog = ProductCatalog.LoadDefaultCatalog();
			IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder,catalog);
			
			HelperSubscription.CanDebug = CanDebug;
			ProductValidator.CanDebug = CanDebug;
			ProductValidator.UseValidate = useValidate;
			ProductValidator.Init();
			
			UnityPurchasing.Initialize(this,builder);
		}
		#endregion
		
		
		#region Implementation
		private static IStoreController m_Controller;
		private static IExtensionProvider m_extensions;
		private static ITransactionHistoryExtensions m_TransactionHistoryExtensions;
		
		#pragma warning disable
		private static IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
		//Dictionary<string, string> google_play_store_product_SKUDetails_json;
		#pragma warning restore
		
		private static IAppleExtensions m_AppleExtensions;
		//Dictionary<string, string> introductory_info_dict;
		
		
		void IStoreListener.OnInitialized(IStoreController controller,IExtensionProvider extensions)
		{
			m_Controller = controller;
			m_extensions = extensions;
			
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
			{
				m_GooglePlayStoreExtensions = m_extensions.GetExtension<IGooglePlayStoreExtensions>();
				//google_play_store_product_SKUDetails_json = m_GooglePlayStoreExtensions.GetProductJSONDictionary();
			}
			else if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore)
			{
				m_AppleExtensions = m_extensions.GetExtension<IAppleExtensions>();
				//introductory_info_dict = m_AppleExtensions.GetIntroductoryPriceDictionary();
			}
			
			OnInit(true);
			
			if(!CanDebug) return;
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
		}
		
		void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
		{
			xDebug.LogExceptionFormat(this,this.name+":OnInitializeFailed:{0}",error);
			OnInit(false);
		}
		
		PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
		{
			currentProduct = args.purchasedProduct;
			bool isValid = ProductValidator.IsValid(args);
			
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPurchaseProcess:{0}:{1}",isValid,args.purchasedProduct.definition.id);
			StAnalytics.LogEvent("IAP","OnPurchaseProcess:"+isValid,args.purchasedProduct.definition.id);
			
			OnPurchase(isValid,args.purchasedProduct.definition.id);
			
			return PurchaseProcessingResult.Complete;
		}
		
		void IStoreListener.OnPurchaseFailed(Product product,PurchaseFailureReason productFailureReason)
		{
			currentProduct = product;
			
			xDebug.LogExceptionFormat(this,this.name+":OnPurchaseFailed:{0}:{1}",productFailureReason,product.definition.id);
			StAnalytics.LogEvent("IAP","OnPurchaseFailed:"+productFailureReason,product.definition.id);
			
			OnPurchase(false,product.definition.id);
		}
		#endregion
		
		
		#region Fuctions
		#region Purchase
		public NodeBool inPurchase;
		public void Purchase(Product product)
		{
			currentProduct = product;
			
			if(product == null) return;
			if(inPurchase.Value) return;
			inPurchase.Value = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Purchase:{0}",product.definition.id);
			
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
		public NodeBool inRestore;
		public void Restore()
		{
			if(inRestore.Value) return;
			inRestore.Value = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Restore");
			
			if(!onInit.Value)
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
				if(CanDebug) Debug.LogWarningFormat(this,this.name+":NotSupported:{0}",StandardPurchasingModule.Instance().appStore);
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
			if(onInit.Value) return m_Controller.products.WithID(key);
			else return null;
		}
		
		public NodeBool onInit;
		private void OnInit(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnInit:{0}",value);
			inInit = false;
			onInit.Value = value;
			if(!value) return;
		}
		
		public NodeBool onPurchase;
		public static Action<bool,string> onPurchaseProduct;
		private void OnPurchase(bool result,string productId)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPurchase:{0}:{1}",result,productId);
			inPurchase.Value = false;
			
			if(!result) StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Failed"));
			else StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Successful"));
			
			onPurchase.Value = result;
			onPurchaseProduct(result,productId);
		}
		
		public NodeBool onRestore;
		private void OnRestoreTrue(bool isContinue)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRestore:True:isContinue:{0}",isContinue);
			StPopupBar.QueueMessage(MnLocalize.GetValue("Restore Successful"));
			inRestore.Value = false;
			onRestore.Value = true;
		}
		
		private void OnRestoreFalse()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRestore:False");
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
		
		public NodeBool onInit = null;
		public override void Init()
		{
			onInit.Value = true;
		}
		
		public NodeBool inPurchase = null;
		public void Purchase(string productId)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Purchase:{0}",productId);
			currentProductId = productId;
			
			StPopupWindow.Reset();
			StPopupWindow.HeaderLocalized("warning");
			StPopupWindow.Body(string.Format("{0}\n{1}\nbuy?",productId,"0.00$"));
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
		public static Action<bool,string> onPurchaseProduct;
		private void OnPurchase(bool result,string productId)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPurchase:{0}:{1}",result,productId);
			currentProductId = productId;
			inPurchase.Value = false;
			
			if(!result) StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Failed"));
			else StPopupBar.QueueMessage(MnLocalize.GetValue("Purchase Successful"));
			
			onPurchase.Value = result;
			onPurchaseProduct(result,productId);
		}
		
		public NodeBool inRestore = null;
		public NodeBool onRestore = null;
		public void Restore()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Restore");
			StPopupBar.QueueMessage(MnLocalize.GetValue("Restore Failed"));
			onRestore.Value = false;
		}
		
		public static string currentProductId = null;
		public string GetProduct(string key)
		{
			if(onInit.Value) return key;
			else return null;
		}
		#pragma warning restore
	}
}
#endif
#endif
#if xLibv3
#if IapUnity
using UnityEngine;
using UnityEngine.Purchasing;
using xLib.Purchasing;
using xLib.Purchasing.Security;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnProduct : SingletonM<MnProduct>, IStoreListener
	{
		[SerializeField]private bool autoRestore = true;
		[SerializeField]private bool useValidate = true;
		[SerializeField]private FakeStoreUIMode fakeStoreUIMode = FakeStoreUIMode.StandardUser;
		
		#region Init
		private bool inInit;
		public override void Init()
		{
			base.Init();
			if(isInit.Value) return;
			if(inInit) return;
			inInit = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Init");
			
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
			
			IsInit(true);
			
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
			IsInit(false);
		}
		
		PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
		{
			currentProduct = args.purchasedProduct;
			bool isValid = ProductValidator.IsValid(args);
			
			if(CanDebug) Debug.LogFormat(this,this.name+":OnPurchaseProcess:{0}:{1}",isValid,args.purchasedProduct.definition.id);
			StAnalytics.LogEvent("IAP","OnPurchaseProcess:"+isValid,args.purchasedProduct.definition.id);
			
			IsPurchase(isValid);
			
			return PurchaseProcessingResult.Complete;
		}
		
		void IStoreListener.OnPurchaseFailed(Product product,PurchaseFailureReason productFailureReason)
		{
			currentProduct = product;
			
			xDebug.LogExceptionFormat(this,this.name+":OnPurchaseFailed:{0}:{1}",productFailureReason,product.definition.id);
			StAnalytics.LogEvent("IAP","OnPurchaseFailed:"+productFailureReason,product.definition.id);
			
			IsPurchase(false);
		}
		#endregion
		
		
		#region Fuctions
		#region Purchase
		private bool inPurchase;
		public void Purchase(Product value)
		{
			if(value==null) return;
			if(inPurchase) return;
			inPurchase = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Waiting");
			
			m_Controller.InitiatePurchase(value);
		}
		#endregion
		
		#region Restore
		private bool inRestore;
		public void Restore()
		{
			if(inRestore) return;
			inRestore = true;
			if(CanDebug) Debug.LogFormat(this,this.name+":Restore");
			
			if(!isInit.Value)
			{
				Init();
				IsRestoreFalse();
				return;
			}
			
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
			{
				IsRestoreTrue(false);
			}
			else if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore)
			{
				m_AppleExtensions.RestoreTransactions(IsRestoreTrue);
			}
			else
			{
				if(CanDebug) Debug.LogWarningFormat(this,this.name+":NotSupported:{0}",StandardPurchasingModule.Instance().appStore);
				IsRestoreFalse();
			}
		}
		#endregion
		#endregion
		
		
		#region Callback
		public static Product currentProduct = null;
		public Product GetProduct(string key)
		{
			if(isInit.Value) return m_Controller.products.WithID(key);
			else return null;
		}
		
		public NodeBool isInit;
		private void IsInit(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsInit:{0}",value);
			inInit = false;
			isInit.Value = value;
			if(!value) return;
			if(autoRestore) Restore();
		}
		
		public NodeBool isPurchase;
		private void IsPurchase(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsPurchase:{0}",value);
			inPurchase = false;
			isPurchase.Value = value;
		}
		
		public NodeBool isRestore;
		private void IsRestoreTrue(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsRestoreTrue");
			if(CanDebug) Debug.LogFormat(this,this.name+":IsRestoreTrueContinue:{0}",value);
			inRestore = false;
			isRestore.Value = true;
		}
		
		private void IsRestoreFalse()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":IsRestoreFalse");
			inRestore = false;
			isRestore.Value = false;
		}
		#endregion
	}
}
#else
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnProduct : SingletonM<MnProduct>
	{
		#pragma warning disable
		[SerializeField]private bool autoRestore = true;
		[SerializeField]private bool useValidate = true;
		[SerializeField]private int fakeStoreUIMode = 1;
		
		public NodeBool isInit;
		public NodeBool isPurchase;
		public NodeBool isRestore;
		
		public void Purchase(string key)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Purchase");
			isPurchase.Value = true;
		}
		
		public void Restore()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Restore");
			isRestore.Value = false;
		}
		#pragma warning restore
	}
}
#endif
#endif
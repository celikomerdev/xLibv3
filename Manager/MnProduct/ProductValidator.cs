#if xLibv3
#if IapUnity
using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace xLib.Purchasing.Security
{
	public static class ProductValidator
	{
		internal static bool CanDebug = true;
		internal static bool UseValidate = true;
		private static CrossPlatformValidator validator;
		
		internal static void Init()
		{
			if(CanDebug) Debug.LogFormat("ProductValidator:Init");
			
			#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
			validator = new CrossPlatformValidator(GooglePlayTangle.Data(),AppleTangle.Data(),UnityChannelTangle.Data(),Application.identifier);
			if(validator == null) xDebug.LogExceptionFormat("ProductValidator:validator=null!");
			#endif
		}
		
		internal static bool IsValid(PurchaseEventArgs args)
		{
			if(Application.isEditor) return true;
			if(!UseValidate) return true;
			
			bool canValidate = false;
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay) canValidate = true;
			if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore) canValidate = true;
			if(!canValidate) return true;
			
			
			IPurchaseReceipt[] receiptArray = null;
			bool receiptResult = false;
			try
			{
				receiptArray = validator.Validate(args.purchasedProduct.receipt);
				if(receiptArray != null) receiptResult = true;
			}
			catch(IAPSecurityException ex)
			{
				xDebug.LogExceptionFormat("ProductValidator:IAPSecurityException:{0}",ex);
			}
			catch(Exception ex)
			{
				xDebug.LogExceptionFormat("ProductValidator:Exception:{0}",ex);
			}
			
			DebugReceipt(receiptArray);
			return receiptResult;
		}
		
		private static void DebugReceipt(IPurchaseReceipt[] receiptArray)
		{
			if(receiptArray == null)
			{
				xDebug.LogExceptionFormat("ProductValidator:ReceiptArray=null");
				return;
			}
			
			if(!CanDebug) return;
			foreach (IPurchaseReceipt productReceipt in receiptArray)
			{
				Debug.LogFormat("productID:{0}",productReceipt.productID);
				Debug.LogFormat("purchaseDate:{0}",productReceipt.purchaseDate);
				Debug.LogFormat("transactionID:{0}",productReceipt.transactionID);
				
				GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
				if (google != null)
				{
					Debug.LogFormat("packageName:{0}",google.packageName);
					Debug.LogFormat("purchaseState:{0}",google.purchaseState);
					Debug.LogFormat("purchaseToken:{0}",google.purchaseToken);
				}
				
				AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
				if (apple != null)
				{
					Debug.LogFormat("originalTransactionIdentifier:{0}",apple.originalTransactionIdentifier);
					Debug.LogFormat("subscriptionExpirationDate:{0}",apple.subscriptionExpirationDate);
					Debug.LogFormat("cancellationDate:{0}",apple.cancellationDate);
					Debug.LogFormat("quantity:{0}",apple.quantity);
				}
				
				UnityChannelReceipt unityChannel = productReceipt as UnityChannelReceipt;
				if (unityChannel != null){}
			}
		}
	}
}
#endif
#endif
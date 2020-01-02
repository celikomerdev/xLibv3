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
			if(!UseValidate) return;
			if(CanDebug) Debug.Log($"ProductValidator:Init");
			
			#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
			validator = new CrossPlatformValidator(GooglePlayTangle.Data(),AppleTangle.Data(),Application.identifier);
			if(validator == null) xLogger.LogException($"ProductValidator:validator:null");
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
				xLogger.LogException($"ProductValidator:IAPSecurityException:{ex}");
			}
			catch(Exception ex)
			{
				xLogger.LogException($"ProductValidator:Exception:{ex}");
			}
			
			DebugReceipt(receiptArray);
			return receiptResult;
		}
		
		private static void DebugReceipt(IPurchaseReceipt[] receiptArray)
		{
			if(receiptArray == null)
			{
				xLogger.LogException($"ProductValidator:ReceiptArray:null");
				return;
			}
			
			if(!CanDebug) return;
			foreach (IPurchaseReceipt productReceipt in receiptArray)
			{
				Debug.Log($"productID:{productReceipt.productID}");
				Debug.Log($"purchaseDate:{productReceipt.purchaseDate}");
				Debug.Log($"transactionID:{productReceipt.transactionID}");
				
				GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
				if (google != null)
				{
					Debug.Log($"packageName:{google.packageName}");
					Debug.Log($"purchaseState:{google.purchaseState}");
					Debug.Log($"purchaseToken:{google.purchaseToken}");
				}
				
				AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
				if (apple != null)
				{
					Debug.Log($"originalTransactionIdentifier:{apple.originalTransactionIdentifier}");
					Debug.Log($"subscriptionExpirationDate:{apple.subscriptionExpirationDate}");
					Debug.Log($"cancellationDate:{apple.cancellationDate}");
					Debug.Log($"quantity:{apple.quantity}");
				}
				
				UnityChannelReceipt unityChannel = productReceipt as UnityChannelReceipt;
				if (unityChannel != null){}
			}
		}
	}
}
#endif
#endif
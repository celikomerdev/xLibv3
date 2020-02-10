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
			TryInit();
			if(CanDebug) Debug.Log($"ProductValidator:Init:{UseValidate}");
		}
		
		private static void TryInit()
		{
			if(!UseValidate) return;
			
			UseValidate = false;
			if(StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay) UseValidate = true;
			if(StandardPurchasingModule.Instance().appStore == AppStore.AppleAppStore) UseValidate = true;
			if(!UseValidate) return;
			
			#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
			if(CanDebug) Debug.Log($"ProductValidator:GooglePlayTangle:{GooglePlayTangle.Data().HashSHA256()}");
			if(CanDebug) Debug.Log($"ProductValidator:AppleTangle:{AppleTangle.Data().HashSHA256()}");
			validator = new CrossPlatformValidator(GooglePlayTangle.Data(),AppleTangle.Data(),Application.identifier);
			if(validator == null) Debug.LogException(new UnityException($"ProductValidator:validator:null"));
			#endif
			
			if(validator==null) UseValidate = false;
			if(Application.isEditor) UseValidate = false;
		}
		
		internal static bool IsValid(PurchaseEventArgs args)
		{
			if(CanDebug) Debug.Log($"ProductValidator:receipt:{args.purchasedProduct.receipt}");
			if(!UseValidate) return true;
			
			try
			{
				IPurchaseReceipt[] receiptArray = validator.Validate(args.purchasedProduct.receipt);
				DebugReceipt(receiptArray);
				return (receiptArray!=null);
			}
			catch(IAPSecurityException ex)
			{
				Debug.LogException(new UnityException($"ProductValidator:IAPSecurityException",ex));
			}
			catch(Exception ex)
			{
				Debug.LogException(new UnityException($"ProductValidator:Exception",ex));
			}
			return false;
		}
		
		private static void DebugReceipt(IPurchaseReceipt[] receiptArray)
		{
			if(receiptArray == null)
			{
				Debug.LogException(new UnityException($"ProductValidator:ReceiptArray:null"));
				return;
			}
			
			if(!CanDebug) return;
			foreach (IPurchaseReceipt productReceipt in receiptArray)
			{
				Debug.Log($"productID:{productReceipt.productID}");
				Debug.Log($"purchaseDate:{productReceipt.purchaseDate}");
				Debug.Log($"transactionID:{productReceipt.transactionID}");

				switch (productReceipt)
				{
					case GooglePlayReceipt google:
						Debug.Log($"packageName:{google.packageName}");
						Debug.Log($"purchaseState:{google.purchaseState}");
						Debug.Log($"purchaseToken:{google.purchaseToken}");
						break;
					case AppleInAppPurchaseReceipt apple:
						Debug.Log($"originalTransactionIdentifier:{apple.originalTransactionIdentifier}");
						Debug.Log($"subscriptionExpirationDate:{apple.subscriptionExpirationDate}");
						Debug.Log($"cancellationDate:{apple.cancellationDate}");
						Debug.Log($"quantity:{apple.quantity}");
						break;
					case UnityChannelReceipt unityChannel:
						break;
				}
			}
		}
	}
}
#endif
#endif
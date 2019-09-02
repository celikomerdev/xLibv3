#if xLibv3
#if IapUnity
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace xLib.Purchasing
{
	public static class HelperSubscription
	{
		public static bool CanDebug = true;
		
		public static SubscriptionInfo GetInfo(Product item)
		{
			if (item.definition.type != ProductType.Subscription)
			{
				if(CanDebug) Debug.Log("the product is not a subscription product");
				return null;
			}
			
			if (item.receipt == null)
			{
				if(CanDebug) Debug.Log("the product should have a valid receipt");
				return null;
			}
			
			if (!IsAvailableForSubscriptionManager(item.receipt))
			{
				if(CanDebug) Debug.Log("This product is not available for SubscriptionManager class, only products that are purchase by 1.19+ SDK can use this class.");
				return null;
			}
			
			//string intro_json = (introductory_info_dict == null || !introductory_info_dict.ContainsKey(item.definition.storeSpecificId)) ? null : introductory_info_dict[item.definition.storeSpecificId];
			SubscriptionManager subscriptionManager = new SubscriptionManager(item,null);
			SubscriptionInfo subscriptionInfo = subscriptionManager.getSubscriptionInfo();
			
			if(CanDebug)
			{
				Debug.Log("----SubscriptionInfo----");
				Debug.Log("product id is: " + subscriptionInfo.getProductId());
				Debug.Log("purchase date is: " + subscriptionInfo.getPurchaseDate());
				Debug.Log("subscription next billing date is: " + subscriptionInfo.getExpireDate());
				Debug.Log("is subscribed? " + subscriptionInfo.isSubscribed().ToString());
				Debug.Log("is expired? " + subscriptionInfo.isExpired().ToString());
				Debug.Log("is cancelled? " + subscriptionInfo.isCancelled());
				Debug.Log("product is in free trial peroid? " + subscriptionInfo.isFreeTrial());
				Debug.Log("product is auto renewing? " + subscriptionInfo.isAutoRenewing());
				Debug.Log("subscription remaining valid time until next billing date is: " + subscriptionInfo.getRemainingTime());
				Debug.Log("is this product in introductory price period? " + subscriptionInfo.isIntroductoryPricePeriod());
				Debug.Log("the product introductory localized price is: " + subscriptionInfo.getIntroductoryPrice());
				Debug.Log("the product introductory price period is: " + subscriptionInfo.getIntroductoryPricePeriod());
				Debug.Log("the number of product introductory price period cycles is: " + subscriptionInfo.getIntroductoryPricePeriodCycles());
			}
			
			return subscriptionInfo;
		}
		
		
		private static bool IsAvailableForSubscriptionManager(string receipt)
		{
			var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
			if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload"))
			{
				Debug.Log("The product receipt does not contain enough information");
				return false;
			}
			var store = (string)receipt_wrapper["Store"];
			var payload = (string)receipt_wrapper["Payload"];
			
			if (payload != null)
			{
				switch (store)
				{
					case GooglePlay.Name:
					{
						var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
						if (!payload_wrapper.ContainsKey("json"))
						{
							Debug.Log("The product receipt does not contain enough information, the 'json' field is missing");
							return false;
						}
						
						var original_json_payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
						if (original_json_payload_wrapper == null || !original_json_payload_wrapper.ContainsKey("developerPayload"))
						{
							Debug.Log("The product receipt does not contain enough information, the 'developerPayload' field is missing");
							return false;
						}
						
						var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
						var developerPayload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
						if (developerPayload_wrapper == null || !developerPayload_wrapper.ContainsKey("is_free_trial") || !developerPayload_wrapper.ContainsKey("has_introductory_price_trial"))
						{
							Debug.Log("The product receipt does not contain enough information, the product is not purchased using 1.19 or later");
							return false;
						}
						
						return true;
					}
					case AppleAppStore.Name:
					case AmazonApps.Name:
					case MacAppStore.Name:
					{
						return true;
					}
					default:
					{
						return false;
					}
				}
			}
			return false;
		}
	}
}
#endif
#endif



// #region Platform
// public bool IsSubscribed(IPurchaseReceipt productReceipt)
// {
// 	bool isActive = false;
// 	AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
// 	GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
// 	bool appleActive = IsSubscribedApple(apple);
// 	bool googleActive = IsSubscribedGoogle(google);
	
// 	if(appleActive || googleActive) isActive = true;
// 	return isActive;
// }

// public bool IsSubscribedGoogle(GooglePlayReceipt receipt)
// {
// 	if (receipt==null) return false;
	
// 	Debug.Log("receipt.purchaseState: "+receipt.purchaseState);
// 	if(receipt.purchaseState == GooglePurchaseState.Purchased) return true;
// 	return false;
// }

// public bool IsSubscribedApple(AppleInAppPurchaseReceipt receipt)
// {
// 	if (receipt == null) return false;
	
// 	Debug.Log("receipt.subscriptionExpirationDate: "+receipt.subscriptionExpirationDate);
// 	Debug.Log("receipt.cancellationDate: "+receipt.cancellationDate);
// 	DateTime compareDate = receipt.subscriptionExpirationDate;
// 	//DateTime compareDate = receipt.cancellationDate;
	
// 	if(DateTime.Compare(DateTime.Now, compareDate) < 0) return true;
// 	return false;
// }
// #endregion
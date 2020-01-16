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
				Debug.LogWarning("the product is not a subscription product");
				return null;
			}
			
			if (item.receipt == null)
			{
				Debug.LogWarning("the product should have a valid receipt");
				return null;
			}
			
			if (!IsAvailableForSubscriptionManager(item.receipt))
			{
				Debug.LogWarning("IsAvailableForSubscriptionManager:false");
				return null;
			}
			
			string productDetails = (MnProduct.productsDetails == null || !MnProduct.productsDetails.ContainsKey(item.definition.storeSpecificId))? null:MnProduct.productsDetails[item.definition.storeSpecificId];
			SubscriptionManager subscriptionManager = new SubscriptionManager(item,productDetails);
			SubscriptionInfo subscriptionInfo = subscriptionManager.getSubscriptionInfo();
			
			if(CanDebug)
			{
				Debug.Log("----SubscriptionInfo----");
				Debug.Log($"getProductId:{subscriptionInfo.getProductId()}");
				
				Debug.Log($"isSubscribed:{subscriptionInfo.isSubscribed()}");
				Debug.Log($"getPurchaseDate:{subscriptionInfo.getPurchaseDate().ToString()}");
				Debug.Log($"getRemainingTime:{subscriptionInfo.getRemainingTime().TotalMinutes}");
				
				
				Debug.Log($"isAutoRenewing:{subscriptionInfo.isAutoRenewing()}");
				Debug.Log($"getSubscriptionPeriod:{subscriptionInfo.getSubscriptionPeriod().TotalMinutes}");
				
				Debug.Log($"isCancelled:{subscriptionInfo.isCancelled()}");
				Debug.Log($"getCancelDate:{subscriptionInfo.getCancelDate().ToString()}");
				
				Debug.Log($"isExpired:{subscriptionInfo.isExpired()}");
				Debug.Log($"getExpireDate:{subscriptionInfo.getExpireDate().ToString()}");
				
				Debug.Log($"isFreeTrial:{subscriptionInfo.isFreeTrial()}");
				Debug.Log($"getFreeTrialPeriod:{subscriptionInfo.getFreeTrialPeriod().TotalMinutes}");
				Debug.Log($"getFreeTrialPeriodString:{subscriptionInfo.getFreeTrialPeriodString()}");
				
				Debug.Log($"isIntroductoryPricePeriod:{subscriptionInfo.isIntroductoryPricePeriod()}");
				Debug.Log($"getIntroductoryPrice:{subscriptionInfo.getIntroductoryPrice()}");
				Debug.Log($"getIntroductoryPricePeriod:{subscriptionInfo.getIntroductoryPricePeriod().TotalMinutes}");
				Debug.Log($"getIntroductoryPricePeriodCycles:{subscriptionInfo.getIntroductoryPricePeriodCycles()}");
				
				Debug.Log($"getSkuDetails:{subscriptionInfo.getSkuDetails()}");
				Debug.Log($"getSubscriptionInfoJsonString:{subscriptionInfo.getSubscriptionInfoJsonString()}");
			}
			
			return subscriptionInfo;
		}
		
		
		private static bool IsAvailableForSubscriptionManager(string receipt)
		{
			var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
			if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload"))
			{
				Debug.LogWarning("The product receipt does not contain enough information");
				return false;
			}
			var store = (string)receipt_wrapper["Store"];
			var payload = (string)receipt_wrapper["Payload"];
			
			if(CanDebug) Debug.Log($"payload:{payload}");
			if (payload != null)
			{
				if(CanDebug) Debug.Log($"store:{store}");
				switch (store)
				{
					case GooglePlay.Name:
					{
						var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
						if (!payload_wrapper.ContainsKey("json"))
						{
							Debug.LogWarning("The product receipt does not contain enough information, the 'json' field is missing");
							return false;
						}
						
						var original_json_payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
						if (original_json_payload_wrapper == null || !original_json_payload_wrapper.ContainsKey("developerPayload"))
						{
							Debug.LogWarning("The product receipt does not contain enough information, the 'developerPayload' field is missing");
							return false;
						}
						
						var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
						var developerPayload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
						if (developerPayload_wrapper == null || !developerPayload_wrapper.ContainsKey("is_free_trial") || !developerPayload_wrapper.ContainsKey("has_introductory_price_trial"))
						{
							Debug.LogWarning("The product receipt does not contain enough information, the product is not purchased using 1.19 or later");
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
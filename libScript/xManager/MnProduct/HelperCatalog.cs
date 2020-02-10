#if xLibv3
#if IapUnity
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace xLib.Purchasing
{
	public static class HelperCatalog
	{
		public static void PopulateConfigurationBuilder(ref ConfigurationBuilder builder, ProductCatalog catalog)
		{
			foreach (var product in catalog.allValidProducts)
			{
				IDs ids = null;
				
				if (product.allStoreIDs.Count > 0)
				{
					ids = new IDs();
					foreach (var storeID in product.allStoreIDs)
					{
						ids.Add(storeID.id, storeID.store);
					}
				}
				
				#if UNITY_2017_2_OR_NEWER
				List<PayoutDefinition> payoutDefinitions = new List<PayoutDefinition>();
				foreach (ProductCatalogPayout payout in product.Payouts)
				{
					payoutDefinitions.Add(new PayoutDefinition(payout.typeString, payout.subtype, payout.quantity, payout.data));
				}
				builder.AddProduct(product.id, product.type, ids, payoutDefinitions.ToArray());
				#else
				builder.AddProduct(product.id, product.type, ids);
				#endif
			}
		}
	}
}
#endif
#endif
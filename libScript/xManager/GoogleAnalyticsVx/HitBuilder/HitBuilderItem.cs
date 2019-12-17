#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderItem : HitBuilder<HitBuilderItem>
	{
		private string transactionID = "";
		private string name = "";
		private string SKU = "";
		private double price;
		private string category = "";
		private long quantity;
		private string currencyCode = "";
		
		internal string GetTransactionID()
		{
			return transactionID;
		}
		
		internal HitBuilderItem SetTransactionID(string transactionID)
		{
			if (transactionID != null)
			{
				this.transactionID = transactionID;
			}
			return this;
		}
		
		internal string GetName()
		{
			return name;
		}
		
		internal HitBuilderItem SetName(string name)
		{
			if (name != null)
			{
				this.name = name;
			}
			return this;
		}
		
		internal string GetSKU()
		{
			return name;
		}
		
		internal HitBuilderItem SetSKU(string SKU)
		{
			if (SKU != null)
			{
				this.SKU = SKU;
			}
			return this;
		}
		
		internal double GetPrice()
		{
			return price;
		}
		
		internal HitBuilderItem SetPrice(double price)
		{
			this.price = price;
			return this;
		}
		
		internal string GetCategory()
		{
			return category;
		}
		
		internal HitBuilderItem SetCategory(string category)
		{
			if (category != null)
			{
				this.category = category;
			}
			return this;
		}
		
		internal long GetQuantity()
		{
			return quantity;
		}
		
		internal HitBuilderItem SetQuantity(long quantity)
		{
			this.quantity = quantity;
			return this;
		}
		
		internal string GetCurrencyCode()
		{
			return currencyCode;
		}
		
		internal HitBuilderItem SetCurrencyCode(string currencyCode)
		{
			if (currencyCode != null)
			{
				this.currencyCode = currencyCode;
			}
			return this;
		}
		
		internal override HitBuilderItem GetThis()
		{
			return this;
		}
		
		internal override HitBuilderItem Validate()
		{
			bool isNull = false;
			
			// if(xApp.CanDebug)
			// {
			// 	Debug.LogFormat("HitBuilderItem:transactionID:{0}",transactionID);
			// 	Debug.LogFormat("HitBuilderItem:name:{0}",name);
			// 	Debug.LogFormat("HitBuilderItem:SKU:{0}",SKU);
			// 	Debug.LogFormat("HitBuilderItem:price:{0}",price);
			// 	Debug.LogFormat("HitBuilderItem:quantity:{0}",quantity);
			// }
			
			if (string.IsNullOrEmpty(transactionID))
			{
				Debug.LogWarningFormat("HitBuilderItem:transactionID:null");
				isNull = true;
			}
			
			if (string.IsNullOrEmpty(name))
			{
				Debug.LogWarningFormat("HitBuilderItem:name:null");
				isNull = true;
			}
			
			if (string.IsNullOrEmpty(SKU))
			{
				Debug.LogWarningFormat("HitBuilderItem:SKU:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderTransaction : HitBuilder<HitBuilderTransaction>
	{
		private string transactionID = "";
		private string affiliation = "";
		private double revenue;
		private double tax;
		private double shipping;
		private string currencyCode = "";
		
		internal string GetTransactionID()
		{
			return transactionID;
		}
		
		internal HitBuilderTransaction SetTransactionID(string transactionID)
		{
			if (transactionID != null)
			{
				this.transactionID = transactionID;
			}
			return this;
		}
		
		internal string GetAffiliation()
		{
			return affiliation;
		}
		
		internal HitBuilderTransaction SetAffiliation(string affiliation)
		{
			if (affiliation != null)
			{
				this.affiliation = affiliation;
			}
			return this;
		}
		
		internal double GetRevenue()
		{
			return revenue;
		}
		
		internal HitBuilderTransaction SetRevenue(double revenue)
		{
			this.revenue = revenue;
			return this;
		}
		
		internal double GetTax()
		{
			return tax;
		}
		
		internal HitBuilderTransaction SetTax(double tax)
		{
			this.tax = tax;
			return this;
		}
		
		internal double GetShipping()
		{
			return shipping;
		}
		
		internal HitBuilderTransaction SetShipping(double shipping)
		{
			this.shipping = shipping;
			return this;
		}
		
		internal string GetCurrencyCode()
		{
			return currencyCode;
		}
		
		internal HitBuilderTransaction SetCurrencyCode(string currencyCode)
		{
			if (currencyCode != null)
			{
				this.currencyCode = currencyCode;
			}
			return this;
		}
		
		internal override HitBuilderTransaction GetThis()
		{
			return this;
		}
		
		internal override HitBuilderTransaction Validate()
		{
			bool isNull = false;
			
			// if(xApp.CanDebug)
			// {
			// 	Debug.LogFormat("HitBuilderTransaction:transactionID:{0}",transactionID);
			// 	Debug.LogFormat("HitBuilderTransaction:affiliation:{0}",affiliation);
			// 	Debug.LogFormat("HitBuilderTransaction:revenue:{0}",revenue);
			// 	Debug.LogFormat("HitBuilderTransaction:tax:{0}",tax);
			// 	Debug.LogFormat("HitBuilderTransaction:shipping:{0}",shipping);
			// }
			
			if (string.IsNullOrEmpty(transactionID))
			{
				Debug.LogWarningFormat("HitBuilderTransaction:transactionID:null");
				isNull = true;
			}
			
			if (string.IsNullOrEmpty(affiliation))
			{
				Debug.LogWarningFormat("HitBuilderTransaction:affiliation:null");
				isNull = true;
			}
			
			if(isNull) return null;
			return this;
		}
	}
}
#endif
#endif
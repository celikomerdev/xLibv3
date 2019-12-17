#if xLibv2
#if xAnalyticsGoogle
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class GoogleAnalyticsVx : BaseWorkM
	{
		[SerializeField]private GoogleAnalyticsPlatform tracker;
		
		#region Logging
		internal void LogScreen(string title)
		{
			HitBuilderAppView builder = new HitBuilderAppView();
			builder.SetScreenName(title);
			LogScreen(builder);
		}
		
		internal void LogEvent(string eventCategory,string eventAction,string eventLabel,string value)
		{
			HitBuilderEvent builder = new HitBuilderEvent();
			builder.SetEventCategory(eventCategory);
			builder.SetEventAction(eventAction);
			builder.SetEventLabel(eventLabel);
			builder.SetEventValue(value);
			LogEvent(builder);
		}
		
		internal void LogTransaction(string transID,string affiliation,double revenue,double tax,double shipping,string currencyCode="")
		{
			HitBuilderTransaction builder = new HitBuilderTransaction();
			builder.SetTransactionID(transID);
			builder.SetAffiliation(affiliation);
			builder.SetRevenue(revenue);
			builder.SetTax(tax);
			builder.SetShipping(shipping);
			builder.SetCurrencyCode(currencyCode);
			LogTransaction(builder);
		}
		
		internal void LogItem(string transID,string name,string sku,string category,double price,long quantity,string currencyCode="")
		{
			HitBuilderItem builder = new HitBuilderItem();
			builder.SetTransactionID(transID);
			builder.SetName(name);
			builder.SetSKU(sku);
			builder.SetCategory(category);
			builder.SetPrice(price);
			builder.SetQuantity(quantity);
			builder.SetCurrencyCode(currencyCode);
			LogItem(builder);
		}
		
		internal void LogSocial(string socialNetwork,string socialAction,string socialTarget)
		{
			HitBuilderSocial builder = new HitBuilderSocial();
			builder.SetSocialNetwork(socialNetwork);
			builder.SetSocialAction(socialAction);
			builder.SetSocialTarget(socialTarget);
			LogSocial(builder);
		}
		
		internal void LogTiming(string timingCategory,long timingInterval,string timingName,string timingLabel)
		{
			HitBuilderTiming builder = new HitBuilderTiming();
			builder.SetTimingCategory(timingCategory);
			builder.SetTimingInterval(timingInterval);
			builder.SetTimingName(timingName);
			builder.SetTimingLabel(timingLabel);
			LogTiming(builder);
		}
		
		internal void LogException(string exceptionDescription,bool isFatal)
		{
			HitBuilderException builder = new HitBuilderException();
			builder.SetExceptionDescription(exceptionDescription);
			builder.SetFatal(isFatal);
			LogException(builder);
		}
		#endregion
		
		
		#region LoggingFinal
		internal void LogScreen(HitBuilderAppView builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogScreen");
			tracker.LogScreen(builder);
		}
		
		internal void LogEvent(HitBuilderEvent builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogEvent");
			tracker.LogEvent(builder);
		}
		
		internal void LogTransaction(HitBuilderTransaction builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogTransaction");
			tracker.LogTransaction(builder);
		}
		
		internal void LogItem(HitBuilderItem builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogItem");
			tracker.LogItem(builder);
		}
		
		internal void LogSocial(HitBuilderSocial builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogSocial");
			tracker.LogSocial(builder);
		}
		
		internal void LogTiming(HitBuilderTiming builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogTiming");
			tracker.LogTiming(builder);
		}
		
		internal void LogException(HitBuilderException builder)
		{
			if(builder.Validate()==null) return;
			
			if(CanDebug) Debug.LogFormat(this,this.name+":LogException");
			tracker.LogException(builder);
		}
		#endregion
	}
}
#endif
#endif
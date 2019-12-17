#if xLibv2
#if xAnalyticsGoogle
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal class GoogleAnalyticsPlatform : BaseWorkM
	{
		[SerializeField]private bool dryRun;
		[SerializeField]private bool anonymizeIP;
		
		
		internal string bundleIdentifier;
		internal string appName;
		internal string appVersion;
		internal string clientId;
		
		internal string trackingCode;
		internal bool trackingCodeSet;
		
		
		private string m_url;
		private Dictionary<Field,object> trackerValues = new Dictionary<Field,object>();
		internal bool startSessionOnNextHit = true;
		internal bool endSessionOnNextHit;
		
		
		internal void InitializeTracker()
		{
			if(!trackingCodeSet) return;
			
			if(string.IsNullOrEmpty(clientId)) clientId = SystemInfo.deviceUniqueIdentifier;
			string screenRes = string.Format("{0}x{1}",Screen.width,Screen.height);
			string language = Application.systemLanguage.ToString();
			
			CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
			foreach(CultureInfo info in cultureInfos)
			{
				if(info.EnglishName == Application.systemLanguage.ToString())
				{
					language = info.Name;
				}
			}
			
			try
			{
				m_url = "https://www.google-analytics.com/collect?v=1"
				+AddRequiredParameter(Fields.LANGUAGE,language)
				+AddRequiredParameter(Fields.SCREEN_RESOLUTION,screenRes)
				+AddRequiredParameter(Fields.APP_NAME,appName)
				+AddRequiredParameter(Fields.TRACKING_ID,trackingCode)
				+AddRequiredParameter(Fields.APP_ID,bundleIdentifier)
				+AddRequiredParameter(Fields.CLIENT_ID,clientId)
				+AddRequiredParameter(Fields.APP_VERSION,appVersion);
				
				if(anonymizeIP) m_url += AddOptionalParameter(Fields.ANONYMIZE_IP,1);
				if(CanDebug) Debug.LogFormat(this,this.name+":BaseURL:{0}",m_url);
			}
			catch(Exception ex)
			{
				xDebug.LogExceptionFormat(this,this.name+":BaseURL:{0}",ex);
			}
		}
		
		internal void SetTrackerVal(Field field,object value)
		{
			trackerValues[field] = value;
		}
		
		internal void SetTrackerVal(string fieldName,object fieldValue)
		{
			Field tempField = new Field(fieldName);
			trackerValues[tempField] = fieldValue;
		}
		
		private string AddTrackerVals()
		{
			if(!trackingCodeSet) return "";
			
			string vals = "";
			foreach(KeyValuePair<Field,object> pair in trackerValues)
			{
				vals += AddOptionalParameter(pair.Key,pair.Value);
			}
			
			return vals;
		}
		
		private void SendGaHitWithMeasurementProtocol(string tempUrl)
		{
			if(string.IsNullOrEmpty(tempUrl))
			{
				xDebug.LogExceptionFormat(this,this.name+":tempUrl:null");
				return;
			}
			if(dryRun) return;
			
			if(startSessionOnNextHit)
			{
				startSessionOnNextHit = false;
				tempUrl += AddOptionalParameter(Fields.SESSION_CONTROL,"start");
			}
			else if(endSessionOnNextHit)
			{
				endSessionOnNextHit = false;
				tempUrl += AddOptionalParameter(Fields.SESSION_CONTROL,"end");
			}
			
			// Add random z to avoid caching
			string newUrl = tempUrl+"&z="+UnityEngine.Random.Range(0,500);
			if(CanDebug) Debug.LogFormat(this,this.name+":NewURL:{0}",newUrl);
			
			MnCoroutine.ins.NewCoroutine(TrySend(new WWW(newUrl)));
		}
		
		internal IEnumerator TrySend(WWW request)
		{
			while(!request.isDone)
			{
				yield return request;
				if(request.responseHeaders.ContainsKey("STATUS"))
				{
					if(request.responseHeaders["STATUS"].Contains("200 OK"))
					{
						if(CanDebug) Debug.LogFormat(this,this.name+":TrySend:{0}",request.responseHeaders["STATUS"]);
					}
					else
					{
						if(CanDebug) Debug.LogWarningFormat(this,this.name+":TrySend:{0}",request.responseHeaders["STATUS"]);
					}
				}
				else
				{
					xDebug.LogExceptionFormat(this,this.name+":TrySend:{0}",request.error);
				}
			}
		}
		
		private string AddRequiredParameter(Field parameter,object value)
		{
			if(!trackingCodeSet) return "";
			if(value==null) return "";
			return parameter+"="+WWW.EscapeURL(value.ToString());
		}
		
		private string AddRequiredParameter(Field parameter,string value)
		{
			if(!trackingCodeSet) return "";
			if(value==null) return "";
			return parameter+"="+WWW.EscapeURL(value);
		}
		
		private string AddOptionalParameter(Field parameter,object value)
		{
			if(!trackingCodeSet) return "";
			if(value==null) return "";
			return parameter+"="+WWW.EscapeURL(value.ToString());
		}
		
		private string AddOptionalParameter(Field parameter,string value)
		{
			if(!trackingCodeSet) return "";
			if(string.IsNullOrEmpty(value)) return "";
			return parameter+"="+WWW.EscapeURL(value);
		}
		
		private string AddCustomVariables<T>(HitBuilder<T> builder)
		{
			if(!trackingCodeSet) return "";
			
			string tempUrl = "";
			foreach(KeyValuePair<int,string> entry in builder.GetCustomDimensions())
			{
				if(entry.Value != null)
				{
					tempUrl += Fields.CUSTOM_DIMENSION.ToString()+entry.Key+"="+WWW.EscapeURL(entry.Value.ToString());
				}
			}
			foreach(KeyValuePair<int,string> entry in builder.GetCustomMetrics())
			{
				if(entry.Value != null)
				{
					tempUrl += Fields.CUSTOM_METRIC.ToString()+entry.Key+"="+WWW.EscapeURL(entry.Value.ToString());
				}
			}
			
			if(CanDebug) Debug.LogFormat(this,this.name+":AddCustomVariables:{0}",tempUrl);
			return tempUrl;
		}
		
		private string AddCampaignParameters<T>(HitBuilder<T> builder)
		{
			if(!trackingCodeSet) return "";
			
			string tempUrl = "";
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_NAME,builder.GetCampaignName());
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_SOURCE,builder.GetCampaignSource());
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_MEDIUM,builder.GetCampaignMedium());
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_KEYWORD,builder.GetCampaignKeyword());
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_CONTENT,builder.GetCampaignContent());
			tempUrl += AddOptionalParameter(Fields.CAMPAIGN_ID,builder.GetCampaignID());
			tempUrl += AddOptionalParameter(Fields.GCLID,builder.GetGclid());
			tempUrl += AddOptionalParameter(Fields.DCLID,builder.GetDclid());
			
			if(CanDebug) Debug.LogFormat(this,this.name+":AddCampaignParameters:{0}",tempUrl);
			return tempUrl;
		}
		
		internal void LogScreen(HitBuilderAppView builder)
		{
			trackerValues[Fields.SCREEN_NAME] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"appview")
			+AddRequiredParameter(Fields.SCREEN_NAME,builder.GetScreenName())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogEvent(HitBuilderEvent builder)
		{
			trackerValues[Fields.EVENT_CATEGORY] = null;
			trackerValues[Fields.EVENT_ACTION] = null;
			trackerValues[Fields.EVENT_LABEL] = null;
			trackerValues[Fields.EVENT_VALUE] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"event")
			+AddOptionalParameter(Fields.EVENT_CATEGORY,builder.GetEventCategory())
			+AddOptionalParameter(Fields.EVENT_ACTION,builder.GetEventAction())
			+AddOptionalParameter(Fields.EVENT_LABEL,builder.GetEventLabel())
			+AddOptionalParameter(Fields.EVENT_VALUE,builder.GetEventValue())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogTransaction(HitBuilderTransaction builder)
		{
			trackerValues[Fields.TRANSACTION_ID] = null;
			trackerValues[Fields.TRANSACTION_AFFILIATION] = null;
			trackerValues[Fields.TRANSACTION_REVENUE] = null;
			trackerValues[Fields.TRANSACTION_SHIPPING] = null;
			trackerValues[Fields.TRANSACTION_TAX] = null;
			trackerValues[Fields.CURRENCY_CODE] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"transaction")
			+AddRequiredParameter(Fields.TRANSACTION_ID,builder.GetTransactionID())
			+AddOptionalParameter(Fields.TRANSACTION_AFFILIATION,builder.GetAffiliation())
			+AddOptionalParameter(Fields.TRANSACTION_REVENUE,builder.GetRevenue())
			+AddOptionalParameter(Fields.TRANSACTION_SHIPPING,builder.GetShipping())
			+AddOptionalParameter(Fields.TRANSACTION_TAX,builder.GetTax())
			+AddOptionalParameter(Fields.CURRENCY_CODE,builder.GetCurrencyCode())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogItem(HitBuilderItem builder)
		{
			trackerValues[Fields.TRANSACTION_ID] = null;
			trackerValues[Fields.ITEM_NAME] = null;
			trackerValues[Fields.ITEM_SKU] = null;
			trackerValues[Fields.ITEM_CATEGORY] = null;
			trackerValues[Fields.ITEM_PRICE] = null;
			trackerValues[Fields.ITEM_QUANTITY] = null;
			trackerValues[Fields.CURRENCY_CODE] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"item")
			+AddRequiredParameter(Fields.TRANSACTION_ID,builder.GetTransactionID())
			+AddRequiredParameter(Fields.ITEM_NAME,builder.GetName())
			+AddOptionalParameter(Fields.ITEM_SKU,builder.GetSKU())
			+AddOptionalParameter(Fields.ITEM_CATEGORY,builder.GetCategory())
			+AddOptionalParameter(Fields.ITEM_PRICE,builder.GetPrice())
			+AddOptionalParameter(Fields.ITEM_QUANTITY,builder.GetQuantity())
			+AddOptionalParameter(Fields.CURRENCY_CODE,builder.GetCurrencyCode())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
				
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogException(HitBuilderException builder)
		{
			trackerValues[Fields.EX_DESCRIPTION] = null;
			trackerValues[Fields.EX_FATAL] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"exception")
			+AddOptionalParameter(Fields.EX_DESCRIPTION,builder.GetExceptionDescription())
			+AddOptionalParameter(Fields.EX_FATAL,builder.IsFatal())
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogSocial(HitBuilderSocial builder)
		{
			trackerValues[Fields.SOCIAL_NETWORK] = null;
			trackerValues[Fields.SOCIAL_ACTION] = null;
			trackerValues[Fields.SOCIAL_TARGET] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"social")
			+AddRequiredParameter(Fields.SOCIAL_NETWORK,builder.GetSocialNetwork())
			+AddRequiredParameter(Fields.SOCIAL_ACTION,builder.GetSocialAction())
			+AddRequiredParameter(Fields.SOCIAL_TARGET,builder.GetSocialTarget())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
		
		internal void LogTiming(HitBuilderTiming builder)
		{
			trackerValues[Fields.TIMING_CATEGORY] = null;
			trackerValues[Fields.TIMING_VALUE] = null;
			trackerValues[Fields.TIMING_LABEL] = null;
			trackerValues[Fields.TIMING_VAR] = null;
			
			string tempUrl = m_url
			+AddRequiredParameter(Fields.HIT_TYPE,"timing")
			+AddOptionalParameter(Fields.TIMING_CATEGORY,builder.GetTimingCategory())
			+AddOptionalParameter(Fields.TIMING_VALUE,builder.GetTimingInterval())
			+AddOptionalParameter(Fields.TIMING_LABEL,builder.GetTimingLabel())
			+AddOptionalParameter(Fields.TIMING_VAR,builder.GetTimingName())
			+AddCustomVariables(builder)
			+AddCampaignParameters(builder)
			+AddTrackerVals();
			
			SendGaHitWithMeasurementProtocol(tempUrl);
		}
	}
}
#endif
#endif
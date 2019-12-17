#if xLibv2
#if xAnalyticsGoogle
using System.Collections.Generic;
using UnityEngine;

namespace xLib.xAnalyticsGoogle
{
	internal abstract class HitBuilder<T>
	{
		private Dictionary<int,string> customDimensions = new Dictionary<int,string>();
		private Dictionary<int,string> customMetrics = new Dictionary<int,string>();
		
		private string campaignName="";
		private string campaignSource="";
		private string campaignMedium="";
		private string campaignKeyword="";
		private string campaignContent="";
		private string campaignID="";
		private string gclid="";
		private string dclid="";
		
		internal abstract T GetThis();
		internal abstract T Validate();
		
		internal T SetCustomDimension(int dimensionNumber,string value)
		{
			customDimensions.Add(dimensionNumber,value);
			return GetThis();
		}
		
		internal Dictionary<int,string> GetCustomDimensions()
		{
			return customDimensions;
		}
		
		internal T SetCustomMetric(int metricNumber,string value)
		{
			customMetrics.Add(metricNumber, value);
			return GetThis();
		}
		
		internal Dictionary<int,string> GetCustomMetrics()
		{
			return customMetrics;
		}
		
		internal string GetCampaignName()
		{
			return campaignName;
		}
		
		internal T SetCampaignName(string campaignName)
		{
			if (campaignName != null)
			{
				this.campaignName = campaignName;
			}
			return GetThis();
		}
		
		internal string GetCampaignSource()
		{
			return campaignSource;
		}
		
		internal T SetCampaignSource(string campaignSource)
		{
			if (campaignSource != null)
			{
				this.campaignSource = campaignSource;
			}
			else
			{
				Debug.LogWarningFormat("HitBuilder:campaignSource:null");
			}
			return GetThis();
		}
		
		internal string GetCampaignMedium()
		{
			return campaignMedium;
		}

		internal T SetCampaignMedium(string campaignMedium)
		{
			if (campaignMedium != null)
			{
				this.campaignMedium = campaignMedium;
			}
			return GetThis();
		}
		
		internal string GetCampaignKeyword()
		{
			return campaignKeyword;
		}
		
		internal T SetCampaignKeyword(string campaignKeyword)
		{
			if (campaignKeyword != null)
			{
				this.campaignKeyword = campaignKeyword;
			}
			return GetThis();
		}
		
		internal string GetCampaignContent()
		{
			return campaignContent;
		}
		
		internal T SetCampaignContent(string campaignContent)
		{
			if (campaignContent != null)
			{
				this.campaignContent = campaignContent;
			}
			return GetThis();
		}
		
		internal string GetCampaignID()
		{
			return campaignID;
		}
		
		internal T SetCampaignID(string campaignID)
		{
			if (campaignID != null)
			{
				this.campaignID = campaignID;
			}
			return GetThis();
		}
		
		internal string GetGclid()
		{
			return gclid;
		}
		
		internal T SetGclid(string gclid)
		{
			if (gclid != null)
			{
				this.gclid = gclid;
			}
			return GetThis();
		}

		internal string GetDclid()
		{
			return dclid;
		}
		
		internal T SetDclid(string dclid)
		{
			if (dclid != null)
			{
				this.dclid = dclid;
			}
			return GetThis();
		}
	}
}
#endif
#endif
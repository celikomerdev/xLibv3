#if xLibv2
#if xAnalyticsGoogle

namespace xLib.xAnalyticsGoogle
{
	internal class Fields
	{
		//General
		internal readonly static Field ANONYMIZE_IP = new Field("&aip");
		internal readonly static Field HIT_TYPE = new Field("&t");
		internal readonly static Field SESSION_CONTROL = new Field("&sc");
		internal readonly static Field SCREEN_NAME = new Field("&cd");
		internal readonly static Field LOCATION = new Field("&dl");
		internal readonly static Field REFERRER = new Field("&dr");
		internal readonly static Field PAGE = new Field("&dp");
		internal readonly static Field HOSTNAME = new Field("&dh");
		internal readonly static Field TITLE = new Field("&dt");
		internal readonly static Field LANGUAGE = new Field("&ul");
		internal readonly static Field ENCODING = new Field("&de");
		
		// System
		internal readonly static Field SCREEN_COLORS = new Field("&sd");
		internal readonly static Field SCREEN_RESOLUTION = new Field("&sr");
		internal readonly static Field VIEWPORT_SIZE = new Field("&vp");
		
		// Application
		internal readonly static Field APP_NAME = new Field("&an");
		internal readonly static Field APP_ID = new Field("&aid");
		internal readonly static Field APP_INSTALLER_ID = new Field("&aiid");
		internal readonly static Field APP_VERSION = new Field("&av");
		
		// Visitor
		internal readonly static Field CLIENT_ID = new Field("&cid");
		internal readonly static Field USER_ID = new Field("&uid");
		
		// Campaign related fields; used in all hits.
		internal readonly static Field CAMPAIGN_NAME = new Field("&cn");
		internal readonly static Field CAMPAIGN_SOURCE = new Field("&cs");
		internal readonly static Field CAMPAIGN_MEDIUM = new Field("&cm");
		internal readonly static Field CAMPAIGN_KEYWORD = new Field("&ck");
		internal readonly static Field CAMPAIGN_CONTENT = new Field("&cc");
		internal readonly static Field CAMPAIGN_ID = new Field("&ci");
		
		// Autopopulated campaign fields
		internal readonly static Field GCLID = new Field("&gclid");
		internal readonly static Field DCLID = new Field("&dclid");
		
		// Event Hit (&t=event)
		internal readonly static Field EVENT_CATEGORY = new Field("&ec");
		internal readonly static Field EVENT_ACTION = new Field("&ea");
		internal readonly static Field EVENT_LABEL = new Field("&el");
		internal readonly static Field EVENT_VALUE = new Field("&ev");
		
		// Social Hit (&t=social)
		internal readonly static Field SOCIAL_NETWORK = new Field("&sn");
		internal readonly static Field SOCIAL_ACTION = new Field("&sa");
		internal readonly static Field SOCIAL_TARGET = new Field("&st");
		
		// Timing Hit (&t=timing)
		internal readonly static Field TIMING_VAR = new Field("&utv");
		internal readonly static Field TIMING_VALUE = new Field("&utt");
		internal readonly static Field TIMING_CATEGORY = new Field("&utc");
		internal readonly static Field TIMING_LABEL = new Field("&utl");
		
		// Exception Hit (&t=exception)
		internal readonly static Field EX_DESCRIPTION = new Field("&exd");
		internal readonly static Field EX_FATAL = new Field("&exf");
		
		// Ecommerce (&t=transaction / &t=item)
		internal readonly static Field CURRENCY_CODE = new Field("&cu");
		internal readonly static Field TRANSACTION_ID = new Field("&ti");
		internal readonly static Field TRANSACTION_AFFILIATION = new Field("&ta");
		internal readonly static Field TRANSACTION_SHIPPING = new Field("&ts");
		internal readonly static Field TRANSACTION_TAX = new Field("&tt");
		internal readonly static Field TRANSACTION_REVENUE = new Field("&tr");
		internal readonly static Field ITEM_SKU = new Field("&ic");
		internal readonly static Field ITEM_NAME = new Field("&in");
		internal readonly static Field ITEM_CATEGORY = new Field("&iv");
		internal readonly static Field ITEM_PRICE = new Field("&ip");
		internal readonly static Field ITEM_QUANTITY = new Field("&iq");
		
		// General Configuration
		internal readonly static Field TRACKING_ID = new Field("&tid");
		internal readonly static Field SAMPLE_RATE = new Field("&sf");
		internal readonly static Field DEVELOPER_ID = new Field("&did");
		
		internal readonly static Field CUSTOM_METRIC = new Field("&cm");
		internal readonly static Field CUSTOM_DIMENSION = new Field("&cd");
		
		// Advertiser Id Fields
		internal readonly static Field ADID = new Field("&adid");
		internal readonly static Field IDFA = new Field("&idfa");
		internal readonly static Field ATE = new Field("&ate");
	}
}
#endif
#endif
#if xLibv2
#if FireBaseAnalytics
using Firebase.Analytics;

namespace xLib.Analytics
{
	public class MnFirebaseAnalytics : SingletonM<MnFirebaseAnalytics>
	{
		public void LogScreen(string value)
		{
			FirebaseAnalytics.SetCurrentScreen(value,"Class");
		}
		
		public void LogEvent(string valueName,string value)
		{
			FirebaseAnalytics.LogEvent(valueName,"Parameter",value);
		}
		
		public void LogEvent(string valueName,double value)
		{
			FirebaseAnalytics.LogEvent(valueName,"Parameter",value);
		}
		
		public void LogEvent(string valueName,int value)
		{
			FirebaseAnalytics.LogEvent(valueName,"Parameter",value);
		}
		
		public void LogEvent(string valueName,long value)
		{
			FirebaseAnalytics.LogEvent(valueName,"Parameter",value);
		}
	}
}
#else
namespace xLib.Analytics
{
	public class MnFirebaseAnalytics : SingletonM<MnFirebaseAnalytics>
	{
		public void LogScreen(string value){}
		public void LogEvent(string valueName, string value){}
		public void LogEvent(string valueName, double value){}
		public void LogEvent(string valueName, int value){}
		public void LogEvent(string valueName, long value){}
	}
}
#endif
#endif
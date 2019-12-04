#if xLibv1
using UnityEngine;

public static class TimeConverter
{
	public static string MinutesToLargest(double minutes)
	{
		float mins = (float)minutes;
		string result="";
		
		if(minutes>1440)
		{
			int days = Mathf.CeilToInt(mins/1440);
			result = days.ToString();
			result += "Days";
		}
		else if(minutes>60)
		{
			int hours = Mathf.CeilToInt(mins/60);
			result = hours.ToString();
			result += "Hours";
		}
		else
		{
			result += Mathf.CeilToInt(mins);
			result += "Mins";
		}
		
		return result;
	}
}
#endif
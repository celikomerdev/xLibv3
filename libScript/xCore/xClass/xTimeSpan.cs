#if xLibv3
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace xLib
{
	[Serializable]
	public struct xTimeSpan : IFormattable
	{
		public TimeSpan timeSpan;
		
		public xTimeSpan(TimeSpan value)
		{
			this.timeSpan = value;
		}
		
		public static implicit operator TimeSpan(xTimeSpan value)
		{
			return value.timeSpan;
		}
		
		public static implicit operator xTimeSpan(TimeSpan value)
		{
			return new xTimeSpan(value);
		}
		
		public override string ToString()
		{
			return timeSpan.ToString();
		}
		
		public string ToString(string @format,IFormatProvider provider)
		{
			if(provider == null) provider = CultureInfo.CurrentCulture;
			string result = "";
			
			Regex reg_exp = new Regex("[A-Z]+|[^A-Z]+");
			MatchCollection matches = reg_exp.Matches(@format);
			foreach (Match piece in matches)
			{
				string piece_format = new string('0',piece.Value.Length);
				
				// if(timeSpan.Ticks<0)
				// {
				// 	switch (piece.Value[0])
				// 	{
				// 		default:
				// 			result += "-";
				// 			break;
				// 	}
				// 	return result;
				// }
				
				switch (piece.Value[0])
				{
					case 'F':
						result += "'"+((int)timeSpan.TotalMilliseconds).ToString(piece_format)+"'";
						break;
					case 'S':
						result += "'"+((int)timeSpan.TotalSeconds).ToString(piece_format)+"'";
						break;
					case 'M':
						result += "'"+((int)timeSpan.TotalMinutes).ToString(piece_format)+"'";
						break;
					case 'H':
						result += "'"+((int)timeSpan.TotalHours).ToString(piece_format)+"'";
						break;
					case 'D':
						result += "'"+((int)timeSpan.TotalDays).ToString(piece_format)+"'";
						break;
						
					default:
						result += piece.Value;
						break;
				}
			}
			
			// result = result.Replace("Max","'"+timeSpan.ToMax()+"'");
			result = timeSpan.ToString(@result,provider);
			return result;
		}
	}
}
#endif

// {0:mm\:ss\:\0\1}
// {0:mm\:ss\:'at'}
// string format = @"dd\:hh\:mm\:ss";
// string formatted = timeSpan.ToString(@format);

// public static string ToStringNew(this TimeSpan ts,string format)
// {
// 	System.Text.StringBuilder sb = new System.Text.StringBuilder();
// 	sb.Append(format);
// 	return sb.ToString();
// }
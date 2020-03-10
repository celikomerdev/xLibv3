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
		
		public string ToString(string format,IFormatProvider provider = null)
		{
			if(provider.IsNull()) provider = CultureInfo.InvariantCulture;
			string result = "";
			
			Regex reg_exp = new Regex("[A-Z.]+|[^A-Z.]+");
			MatchCollection matches = reg_exp.Matches(format);
			foreach (Match piece in matches)
			{
				string piece_format = Regex.Replace(piece.Value,"[A-Z]","0");
				
				switch (piece.Value[0])
				{
					case '+':
						if(timeSpan.Ticks>0) result += "'+'";
						else result += "'-'";
						break;
					case '-':
						if(timeSpan.Ticks>0) result += "'-'";
						else result += "'+'";
						break;
					case 'F':
						result += "'"+timeSpan.TotalMilliseconds.ToString(piece_format)+"'";
						break;
					case 'S':
						result += "'"+timeSpan.TotalSeconds.ToString(piece_format)+"'";
						break;
					case 'M':
						result += "'"+timeSpan.TotalMinutes.ToString(piece_format)+"'";
						break;
					case 'H':
						result += "'"+timeSpan.TotalHours.ToString(piece_format)+"'";
						break;
					case 'D':
						result += "'"+timeSpan.TotalDays.ToString(piece_format)+"'";
						break;
					case 'X':
						result += "'"+timeSpan.ToMax(piece_format)+"'";
						break;
					default:
						result += piece.Value;
						break;
				}
			}
			
			result = timeSpan.ToString(result,provider);
			return result;
		}
	}
}
#endif
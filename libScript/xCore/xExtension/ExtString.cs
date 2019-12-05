#if xLibv3
using System.Text;
using System.Text.RegularExpressions;

namespace xLib
{
	public static class ExtString
	{
		public static string FormatSafe(this string value,string[] args)
		{
			string formatted = value;
			formatted = formatted.Replace("{ ","{");
			formatted = formatted.Replace(" }","}");
			
			for(int i=0; i<args.Length; i++)
			{
				formatted = formatted.Replace("{"+i+"}",args[i]);
			}
			return formatted;
		}
		
		public static byte[] GetASCIIBytes(this string value)
		{
			byte[] result = new byte[value.Length];
			for (int i = 0; i < value.Length; ++i)
			{
				char ch = value[i];
				result[i] = (byte)((ch < (char)0x80) ? ch : '?');
			}
			return result;
		}
		
		public static string Rasterize(this string value)
		{
			string temp = value.RasterizeLocalize();
			return temp;
		}
		
		// private const string regexFormatLocalize = @"\b<loc>\S*</loc>\b";
		private const string regexFormatLocalize = "<loc>(.*?)</loc>";
		private static string RasterizeLocalize(this string value)
		{
			Regex regex = new Regex(regexFormatLocalize);
			return value;
		}
		
		public static string RemoveSpecials(this string input)
		{
			string returnValue = input;
			returnValue = returnValue.Replace("\n","");
			returnValue = returnValue.Replace("\r","");
			returnValue = returnValue.Replace("\t","");
			return returnValue;
		}
		
		public static string RemoveNewline(this string input)
		{
			string returnValue = input;
			returnValue = returnValue.Replace("\n","");
			returnValue = returnValue.Replace("\r","");
			return returnValue;
		}
		
		public static string RemoveWhiteSpace(this string input)
		{
			return Regex.Replace(input,@"\s+",string.Empty);
		}
		
		#region ToStringx2
		public static string ToStringx2(this byte[] input)
		{
			var stringBuilder = new StringBuilder();
			foreach (byte element in input)
			{
				stringBuilder.Append(element.ToString("x2"));
			}
			return stringBuilder.ToString();
		}
		#endregion
	}
}
#endif
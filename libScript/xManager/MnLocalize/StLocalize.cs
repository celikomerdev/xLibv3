#if xLibv3
using System.Text.RegularExpressions;

namespace xLib
{
	public static class StLocalize
	{
		private const string pattern = @"(?x){(?'name'\w+):(?'value'(?>(?:[^{}]+|(?'open'{)|(?'close-open'}))*)(?(open)(?!)))}";
		public static string Localize(string input)
		{
			MatchCollection matchCollection = Regex.Matches(input,pattern);
			if(matchCollection.Count==0) return input;
			
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				string key = match.Groups["name"].Value;
				string value = match.Groups["value"].Value;
				input = input.Replace($":{value}","");
			}
			
			input = MnLocalize.GetValue(input);
			
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				string key = match.Groups["name"].Value;
				string value = match.Groups["value"].Value;
				input = input.Replace($"{{{key}}}",Localize(value));
			}
			return input;
		}
		
		// // private const string regexFormatLocalize = @"\b<loc>\S*</loc>\b";
		// private const string regexFormatLocalize = "<loc>(.*?)</loc>";
		// private static string RasterizeLocalize(this string value)
		// {
		// 	Regex regex = new Regex(regexFormatLocalize);
		// 	return value;
		// }
	}
}
#endif
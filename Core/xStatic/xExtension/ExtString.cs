#if xLibv2
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
	}
}
#endif
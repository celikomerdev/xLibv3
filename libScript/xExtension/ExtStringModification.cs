#if xLibv3
using System;
using System.Globalization;

namespace xLib
{
	[Serializable]
	public static class ExtStringModification
	{
		public static string ToTitleExt(this string value)
		{
			if(string.IsNullOrEmpty(value)) return string.Empty;
			return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);
		}
		
		public static string ToUpperExt(this string value)
		{
			if(string.IsNullOrEmpty(value)) return string.Empty;
			return CultureInfo.CurrentUICulture.TextInfo.ToUpper(value);
		}
		
		public static string ToLowerExt(this string value)
		{
			if(string.IsNullOrEmpty(value)) return string.Empty;
			return CultureInfo.CurrentUICulture.TextInfo.ToLower(value);
		}
		
		public static string ApplyModificationExt(this string value,StringModification modification)
		{
			switch(modification)
			{
				case StringModification.ToTitle:
					return value.ToTitleExt();
				case StringModification.ToUpper:
					return value.ToUpperExt();
				case StringModification.ToLower:
					return value.ToLowerExt();
			}
			return value;
		}
	}
	
	public enum StringModification
	{
		DontModify,
		ToTitle,
		ToUpper,
		ToLower
	}
}
#endif
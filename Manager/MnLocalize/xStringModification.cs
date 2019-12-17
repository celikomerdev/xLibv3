#if xLibv3
using System;

namespace xLib
{
	[Serializable]
	public class xStringModification
	{
		public static string ApplyModification(string value,StringModification modification)
		{
			switch (modification)
			{
				case StringModification.ToUpper:
					value = value.ToUpper();
					break;
				case StringModification.ToLower:
					value = value.ToLower();
					break;
				#if I2Loc
				case StringModification.ToUpperFirst:
					value = I2.Loc.GoogleTranslation.UppercaseFirst(value);
					break;
				case StringModification.ToTitle:
					value = I2.Loc.GoogleTranslation.TitleCase(value);
					break;
				#endif
			}
			return value;
		}
	}
	
	public enum StringModification
	{
		DontModify,
		ToUpper,
		ToLower,
		ToUpperFirst,
		ToTitle
	}
}
#endif
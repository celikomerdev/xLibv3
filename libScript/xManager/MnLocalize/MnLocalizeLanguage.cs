#if xLibv3
namespace xLib.ToolLocalize
{
	public class MnLocalizeLanguage : BaseM
	{
		public string Language
		{
			set
			{
				MnLocalize.Language = value;
			}
			get
			{
				return MnLocalize.Language;
			}
		}
		
		public int LanguageIndex
		{
			set
			{
				MnLocalize.LanguageIndex = value;
			}
			get
			{
				return MnLocalize.LanguageIndex;
			}
		}
	}
}
#endif
#if xLibv3
#if PackUI
using UnityEngine.UI;

namespace xLib.ToolLocalize
{
	public class MnLocalizeDropdown : BaseM
	{
		public Dropdown dropdown;
		
		private void Awake()
		{
			int tempInt = MnLocalize.LanguageIndex;
			string tempString = MnLocalize.Language;
			
			if(!dropdown) return;
			dropdown.AddOptions(MnLocalize.GetLanguages());
			dropdown.value = MnLocalize.LanguageIndex;
		}
		
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
#endif
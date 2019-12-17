#if xLibv3
#if I2Loc
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnLocalize : SingletonM<MnLocalize>
	{
		[SerializeField]private StringModification modification = StringModification.ToUpper;
		
		public static string GetValue(string key)
		{
			string temp = LocalizationManager.GetTranslation(key);
			if(string.IsNullOrEmpty(temp)) temp = key;
			temp = xStringModification.ApplyModification(temp,ins.modification);
			return temp;
		}
		
		public static List<string> GetLanguages()
		{
			return LocalizationManager.GetAllLanguages();
		}
		
		[SerializeField]private NodeString language;
		public static string Language
		{
			get
			{
				ins.language.Value = LocalizationManager.CurrentLanguage;
				return LocalizationManager.CurrentLanguage;
			}
			set
			{
				ins.language.Value = value;
				LocalizationManager.CurrentLanguage = value;
			}
		}
		
		[SerializeField]private NodeInt languageIndex;
		public static int LanguageIndex
		{
			get
			{
				List<string> languages = GetLanguages();
				for (int i = 0; i < languages.Count; i++)
				{
					if(Language != languages[i]) continue;
					ins.languageIndex.Value = i;
					return i;
				}
				ins.languageIndex.Value = 0;
				return 0;
			}
			set
			{
				ins.languageIndex.Value = value;
				Language = GetLanguages()[value];
			}
		}
	}
}
#else
using System.Collections.Generic;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnLocalize : SingletonM<MnLocalize>
	{
		[SerializeField]private StringModification modification = StringModification.ToUpper;
		
		#region Public
		public static string GetValue(string key)
		{
			string temp = key;
			if(string.IsNullOrEmpty(temp)) temp = key;
			temp = xStringModification.ApplyModification(temp,ins.modification);
			return temp;
		}
		
		public static List<string> GetLanguages()
		{
			List<string> languages = new List<string>{"english"};
			return languages;
		}
		
		[SerializeField]private NodeString language;
		public static string Language
		{
			get
			{
				return "english";
			}
			set
			{
			}
		}
		
		[SerializeField]private NodeInt languageIndex;
		public static int LanguageIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
		#endregion
	}
}
#endif
#endif
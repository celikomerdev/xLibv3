#if xLibv3
#if I2Loc
using System.Collections.Generic;
using System.Globalization;
using I2.Loc;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnLocalize : SingletonM<MnLocalize>
	{
		[SerializeField]private StringModification stringModification = StringModification.DontModify;
		public static string GetValue(string key)
		{
			string temp = LocalizationManager.GetTranslation(key);
			if(string.IsNullOrEmpty(temp)) temp = key;
			if(!ins) return temp;
			return temp.ApplyModificationExt(ins.stringModification);
		}
		
		public static List<string> GetLanguages()
		{
			return LocalizationManager.GetAllLanguages();
		}
		
		[SerializeField]private NodeString language = null;
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
		
		[SerializeField]private NodeInt languageIndex = null;
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
		
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return LocalizationManager.CurrentCulture;
			}
			set
			{
				if(value == null) value = CultureInfo.InvariantCulture;
				if(CultureInfo.CurrentUICulture == value) return;
				CultureInfo.CurrentUICulture = value;
			}
		}
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void TryRegister()
		{
			LocalizationManager.OnLocalizeEvent += OnLocalize;
			OnLocalize();
		}
		
		[SerializeField]public NodeVoid eventLocalize = null;
		private static void OnLocalize()
		{
			CurrentUICulture = CurrentUICulture;
			ins?.eventLocalize.Call();
		}
	}
}
#else
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnLocalize : SingletonM<MnLocalize>
	{
		[SerializeField]private StringModification stringModification = StringModification.DontModify;
		public static string GetValue(string key)
		{
			if(!ins) return key;
			return key.ApplyModificationExt(ins.stringModification);
		}
		
		public static List<string> GetLanguages()
		{
			List<string> languages = new List<string>{"English"};
			return languages;
		}
		
		[SerializeField]private NodeString language;
		public static string Language
		{
			get
			{
				return "English";
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
		
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return CultureInfo.CurrentUICulture;
			}
			set
			{
				if(value == null) value = CultureInfo.InvariantCulture;
				if(CultureInfo.CurrentUICulture == value) return;
				CultureInfo.CurrentUICulture = value;
			}
		}
		
		[SerializeField]public NodeVoid eventLocalize = null;
	}
}
#endif
#endif
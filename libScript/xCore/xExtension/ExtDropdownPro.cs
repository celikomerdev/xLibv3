#if xLibv3
#if TMPro
using TMPro;

namespace xLib
{
	public static class ExtDropdownPro
	{
		public static void SetValueWithString(this TMP_Dropdown dropdown,string stringValue)
		{
			dropdown.value = dropdown.FindOptionIndex(stringValue);
		}
		
		public static int FindOptionIndex(this TMP_Dropdown dropdown,string stringValue)
		{
			for (int i = 0; i < dropdown.options.Count; i++)
			{
				if (dropdown.options[i].text == stringValue)
				{
					return i;
				}
			}
			return 0;
		}
	}
}
#endif
#endif
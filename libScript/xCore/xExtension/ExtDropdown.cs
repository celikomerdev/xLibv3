#if xLibv3
#if ModUI
using UnityEngine.UI;

namespace xLib
{
	public static class ExtDropdown
	{
		public static void SetValueWithString(this Dropdown dropdown,string stringValue)
		{
			dropdown.value = dropdown.FindOptionIndex(stringValue);
		}
		
		public static int FindOptionIndex(this Dropdown dropdown,string stringValue)
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
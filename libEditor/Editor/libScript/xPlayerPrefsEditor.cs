#if xLibv3
using UnityEditor;

namespace xLib
{
	public class xPlayerPrefsEditor
	{
		// [MenuItem("xLib/xPlayerPrefs/Delete/PlayerPrefs")]
		// public static void DeletePlayerPrefs()
		// {
		// 	xPlayerPrefs.DeleteAll();
		// }
		
		[MenuItem("xLib/xPlayerPrefs/Delete/Persistent")]
		public static void DeletePersistent()
		{
			xPersistentData.DeleteAll();
		}
		
		[MenuItem("xLib/xPlayerPrefs/Open/Persistent")]
		public static void OpenPersistent()
		{
			EditorUtility.RevealInFinder(xApp.xPath(""));
		}
	}
}
#endif
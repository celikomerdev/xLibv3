#if xLibv3
using UnityEditor;
using UnityEngine;

namespace xLib
{
	public class AssetDatabaseEditor
	{
		[MenuItem("xLib/xAssetDatabase/SetDirty")]
		public static void KeyGuid()
		{
			Object[] selectionArray = Selection.GetFiltered<Object>(SelectionMode.Unfiltered);
			foreach(Object selection in selectionArray)
			{
				UnityEditor.EditorUtility.SetDirty(selection);
			}
			AssetDatabaseEditor.SaveAssets();
		}
		
		[MenuItem("xLib/xAssetDatabase/SaveAssets")]
		public static void SaveAssets()
		{
			Debug.LogFormat("xAssetDatabaseEditor:SaveAssets");
			AssetDatabase.SaveAssets();
		}
	}
}
#endif
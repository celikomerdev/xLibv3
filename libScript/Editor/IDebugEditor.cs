#if xLibv3
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace xLib
{
	public class IDebugEditor
	{
		[MenuItem("xLib/IDebug/True")]
		public static void SetTrue()
		{
			SetValue(true);
		}
		
		[MenuItem("xLib/IDebug/False")]
		public static void SetFalse()
		{
			SetValue(false);
		}
		
		private static void SetValue(bool value)
		{
			Object[] arrayObject = Selection.GetFiltered<Object>(SelectionMode.Unfiltered);
			IDebug[] arrayGeneric = arrayObject.GetGenericsInChildrenArray<IDebug>(true);
			
			for (int i = 0; i < arrayGeneric.Length; i++)
			{
				arrayGeneric[i].CanDebug = value;
			}
			
			Debug.LogFormat("count:{0}",arrayGeneric.Length);
			AssetDatabaseEditor.SaveAssets();
		}
	}
}
#endif
#endif
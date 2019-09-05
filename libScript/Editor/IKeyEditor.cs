#if xLibv3
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace xLib
{
	public class IKeyEditor
	{
		[MenuItem("xLib/IKey/KeyName")]
		public static void KeyName()
		{
			Object[] arrayObject = Selection.GetFiltered<Object>(SelectionMode.Unfiltered);
			IKey[] arrayGeneric = arrayObject.GetGenericsArray<IKey>();
			
			for (int i = 0; i < arrayGeneric.Length; i++)
			{
				arrayGeneric[i].KeyName();
			}
			
			Debug.LogFormat("count:{0}",arrayGeneric.Length);
			AssetDatabaseEditor.SaveAssets();
		}
		
		[MenuItem("xLib/IKey/KeyGuid")]
		public static void KeyGuid()
		{
			Object[] arrayObject = Selection.GetFiltered<Object>(SelectionMode.Unfiltered);
			IKey[] arrayGeneric = arrayObject.GetGenericsArray<IKey>();
			
			for (int i = 0; i < arrayGeneric.Length; i++)
			{
				arrayGeneric[i].KeyGuid();
			}
			
			Debug.LogFormat("count:{0}",arrayGeneric.Length);
			AssetDatabaseEditor.SaveAssets();
		}
	}
}
#endif
#endif
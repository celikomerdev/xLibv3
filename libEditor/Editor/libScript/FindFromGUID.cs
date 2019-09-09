#if xLibv3
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace xLib.libEditor
{
	public class FindFromGUID : EditorWindow
	{
		private string guid;
		
		[MenuItem("xLib/Window/FindFromGUID")]
		private static void Init()
		{
			FindFromGUID window = (FindFromGUID)EditorWindow.GetWindow(typeof(FindFromGUID));
			window.titleContent.text = "FindFromGUID";
			window.Show();
		}
		
		private void OnGUI()
		{
			GUILayout.Space(10);
			guid = EditorGUILayout.TextField("guid",guid);
			
			GUILayout.Space(10);
			if(GUILayout.Button("Find")){ Find(); };
		}
		
		#region Custom
		private void Find()
		{
			string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
			
			if(string.IsNullOrEmpty(path))
			{
				Debug.LogWarningFormat("not found");
			}
			else
			{
				Object obj = AssetDatabase.LoadAssetAtPath(path,typeof(Object));
				Selection.activeObject = obj;
				Debug.LogWarningFormat(obj,obj.name+":found:{0}",path);
			}
		}
		#endregion
	}
}
#endif
#endif
#if xLibv3
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace xLib.libEditor.Tool
{
	public class FindMissingCustom : EditorWindow 
	{
		private static int countGameObject;
		private static int countComponent;
		private static int countMissing;
		
		[MenuItem("xLib/Window/FindMissing")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(FindMissingCustom));
		}
		
		public void OnGUI()
		{
			if (GUILayout.Button("FindMissings"))
			{
				Work();
			}
		}
		
		private static void Work()
		{
			countGameObject = 0;
			countComponent = 0;
			countMissing = 0;
			
			GameObject[] go = Selection.gameObjects;
			foreach (GameObject g in go)
			{
				FindInGameObject(g);
			}
			
			Debug.LogFormat("Searched, GameObjects:{0}, Components:{1}, Missing:{2}",countGameObject,countComponent,countMissing);
		}
		
		private static void FindInGameObject(GameObject gameObject)
		{
			countGameObject++;
			
			Component[] components = gameObject.GetComponents<Component>();
			// UnityEngine.UI.Image[] components = gameObject.GetComponents<UnityEngine.UI.Image>();
			
			for (int i = 0; i < components.Length; i++)
			{
				countComponent++;
				
				if(components[i] == null)
				// if(components[i].sprite == null)
				{
					countMissing++;
					Debug.LogWarningFormat(gameObject,gameObject.name+":Index:{0}",i);
				}
			}
			
			foreach (Transform childT in gameObject.transform)
			{
				FindInGameObject(childT.gameObject);
			}
		}
	}
}
#endif
#endif
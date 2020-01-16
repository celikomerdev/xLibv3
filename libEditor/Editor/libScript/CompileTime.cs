#if xLibv3
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

//https://answers.unity.com/questions/1131497/how-to-measure-the-amount-of-time-it-takes-for-uni.html
namespace xLib.libEditor
{
	//[InitializeOnLoad]
	internal class CompileTime : EditorWindow
	{
		private static bool isTrackingTime = false;
		private static double startTime = 0;
		
		static CompileTime()
		{
			EditorApplication.update += Update;
			startTime = PlayerPrefs.GetFloat("CompileStartTime",0);
			if (startTime > 0) isTrackingTime = true;
		}
		
		private static void Update()
		{
			if (EditorApplication.isCompiling && !isTrackingTime)
			{
				startTime = EditorApplication.timeSinceStartup;
				PlayerPrefs.SetFloat("CompileStartTime",(float)startTime);
				isTrackingTime = true;
			}
			else if (!EditorApplication.isCompiling && isTrackingTime)
			{
				isTrackingTime = false;
				double finishTime = EditorApplication.timeSinceStartup;
				double compileTime = finishTime - startTime;
				PlayerPrefs.DeleteKey("CompileStartTime");
				Debug.Log($"{compileTime.ToString("F3")}:compileTime");
			}
		}
	}
}
#endif
#endif
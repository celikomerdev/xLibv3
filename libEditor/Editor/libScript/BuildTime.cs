#if xLibv3
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace xLib.libEditor
{
	[InitializeOnLoad]
	internal class BuildTime : IPreprocessBuildWithReport,IProcessSceneWithReport,IPostprocessBuildWithReport
	{
		int IOrderedCallback.callbackOrder
		{
			get
			{
				return 0;
			}
		}
		
		void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
		{
			Debug.Log($"{DateTime.Now.ToString()}:OnPreprocessBuild");
			EditorPrefs.SetString("BuildStartTime",DateTime.UtcNow.ToString());
		}
		
		void IProcessSceneWithReport.OnProcessScene(Scene scene, BuildReport report)
		{
			Debug.Log($"{DateTime.Now.ToString()}:OnProcessScene:{scene.name}");
		}
		
		void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
		{
			DateTime dateTime = DateTime.Parse(EditorPrefs.GetString("BuildStartTime",DateTime.UtcNow.ToString()));
			TimeSpan timeSpan = DateTime.UtcNow - dateTime;
			Debug.Log($"{DateTime.Now.ToString()}:OnPostprocessBuild:");
			Debug.Log($"{timeSpan.TotalSeconds.ToString("F3")}:OnPostprocessBuild");
		}
	}
}
#endif
#endif
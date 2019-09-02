#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnLoader : SingletonM<MnLoader>
	{
		#region Field
		[Header("Manager")]
		[SerializeField]private NodeInt loadedLevel = null;
		[SerializeField]private NodeFloat loadingProgress = null;
		
		[SerializeField]private NodeBool loadingEarly = null;
		[SerializeField]private NodeBool loadingLate = null;
		[SerializeField]private float delay = 1;
		#endregion
		
		private void Update()
		{
			loadingProgress.Value = Mathf.Lerp(loadingProgress.Value,progress,Time.unscaledDeltaTime*8);
		}
		
		#region Public
		public void LoadLevel(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LoadLevel:{0}",value);
			LoadLevel(SceneUtility.GetBuildIndexByScenePath(value));
		}
		
		public void LoadLevel(int value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LoadLevel:{0}",value);
			
			if(value<0)
			{
				xDebug.LogExceptionFormat(this,this.name+":LoadLevel:{0}",value);
				return;
			}
			
			if(inLoad) return;
			inLoad = true;
			loadingProgress.Value = 0;
			UpdateProgress(0);
			MnCoroutine.NewCoroutine(LoadLevelAsync(value));
		}
		#endregion
		
		#region Private
		private float progress;
		private void UpdateProgress(float value)
		{
			progress = value + 0.1f;
		}
		#endregion
		
		#region System
		private bool inLoad;
		private IEnumerator LoadLevelAsync(int value)
		{
			loadingEarly.Value = true;
			yield return new WaitForSecondsRealtime(delay);
			
			loadingLate.Value = true;
			yield return new WaitForEndOfFrame();
			
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
			while (!asyncLoad.isDone)
			{
				UpdateProgress(asyncLoad.progress);
				loadingProgress.Value = Mathf.Lerp(loadingProgress.Value,progress,Time.unscaledDeltaTime*3);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
			enabled = true;
			
			inLoad = false;
			loadedLevel.Value = value;
			
			loadingEarly.Value = false;
			yield return new WaitForSecondsRealtime(delay);
			
			loadingLate.Value = false;
			yield return new WaitForEndOfFrame();
			
			enabled = false;
			yield return null;
		}
		#endregion
	}
}
#endif
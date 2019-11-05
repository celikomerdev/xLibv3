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
		[SerializeField]private bool isAsync = true;
		[SerializeField]public NodeInt loadedLevel = null;
		[SerializeField]public NodeFloat loadingProgress = null;
		
		[SerializeField]public NodeBool loadingVisual = null;
		[SerializeField]public NodeBool loadingReal = null;
		[SerializeField]private float delay = 1;
		#endregion
		
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
			MnCoroutine.NewCoroutine(LoadLevelAsync(value));
		}
		#endregion
		
		#region System
		private bool inLoad;
		private IEnumerator LoadLevelAsync(int value)
		{
			if(inLoad) yield break;
			inLoad = true;
			
			loadingProgress.Value = 0;
			loadingVisual.Value = true;
			yield return new WaitForSecondsRealtime(delay);
			
			loadingReal.Value = true;
			yield return null;
			
			if(!isAsync)
			{
				SceneManager.LoadScene(value);
				loadingProgress.Value = 0.5f;
			}
			else
			{
				AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
				while(!asyncLoad.isDone)
				{
					loadingProgress.Value = asyncLoad.progress + 0.1f;
					yield return null;
				}
			}
			
			yield return null;
			inLoad = false;
			loadedLevel.Value = value;
			loadingReal.Value = false;
			loadingVisual.Value = false;
		}
		#endregion
	}
}
#endif
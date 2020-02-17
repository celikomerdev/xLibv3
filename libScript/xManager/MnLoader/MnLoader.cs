#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using xLib.xValueClass;

namespace xLib
{
	public class MnLoader : SingletonM<MnLoader>
	{
		#region Field
		[Header("Manager")]
		[SerializeField]private bool isAsync = false;
		[SerializeField]private float delayStart = 1;
		[SerializeField]private float delayEnd = 1;
		[SerializeField]private ThreadPriority backgroundLoadingPriority = ThreadPriority.Normal;
		
		[SerializeField]public NodeInt loadedLevel = null;
		[SerializeField]public NodeFloat loadingProgress = null;
		
		[SerializeField]public NodeBool loadingVisual = null;
		[SerializeField]public NodeBool loadingReal = null;
		#endregion
		
		#region Public
		public void LoadLevel(string value)
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadLevel:{value}",this);
			LoadLevel(SceneUtility.GetBuildIndexByScenePath(value));
		}
		
		public void LoadLevel(int value)
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadLevel:{value}",this);
			
			if(value<0)
			{
				Debug.LogException(new UnityException($"{this.name}:LoadLevel:{value}"),this);
				return;
			}
			MnCoroutine.ins.NewCoroutine(LoadLevelAsync(value),CanDebug);
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
			yield return new WaitForSecondsRealtime(delayStart);
			
			loadingReal.Value = true;
			ThreadPriority backgroundLoadingPriorityCache = Application.backgroundLoadingPriority;
			Application.backgroundLoadingPriority = backgroundLoadingPriority;
			yield return new WaitForEndOfFrame();
			
			if(!isAsync)
			{
				loadingProgress.Value = 0.5f;
				yield return new WaitForEndOfFrame();
				SceneManager.LoadScene(value);
				yield return new WaitForEndOfFrame();
			}
			else
			{
				AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
				while(!asyncLoad.isDone)
				{
					loadingProgress.Value = asyncLoad.progress + 0.1f;
					yield return new WaitForEndOfFrame();
				}
			}
			yield return new WaitForEndOfFrame();
			Application.backgroundLoadingPriority = backgroundLoadingPriorityCache;
			inLoad = false;
			
			yield return new WaitForSecondsRealtime(delayEnd);
			loadingProgress.Value = 1f;
			loadedLevel.Value = value;
			loadingReal.Value = false;
			loadingVisual.Value = false;
		}
		#endregion
	}
}
#endif
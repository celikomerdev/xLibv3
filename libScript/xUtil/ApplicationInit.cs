#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using xLib.EventClass;

namespace xLib
{
	public class ApplicationInit : BaseMainM
	{
		public void Call()
		{
			StartCoroutine(LoadLevelAsync(1));
		}
		
		[SerializeField]private bool isAsync = false;
		[SerializeField]private EventUnity finished = new EventUnity();
		private IEnumerator LoadLevelAsync(int value)
		{
			Application.backgroundLoadingPriority = ThreadPriority.High;
			yield return null;
			
			if(!isAsync)
			{
				yield return null;
				SceneManager.LoadScene(value);
				yield return null;
			}
			else
			{
				AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
				while(!asyncLoad.isDone)
				{
					yield return null;
				}
			}
			
			yield return null;
			finished.Invoke();
		}
	}
}
#endif
#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using xLib.EventClass;

namespace xLib
{
	public class ApplicationInit : BaseM
	{
		public void Call()
		{
			StartCoroutine(LoadLevelAsync(1));
		}
		
		[SerializeField]private EventUnity finished = new EventUnity();
		private IEnumerator LoadLevelAsync(int value)
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
			while(!asyncLoad.isDone)
			{
				yield return new WaitForEndOfFrame();
			}
			
			yield return new WaitForEndOfFrame();
			finished.Invoke();
			yield return null;
		}
	}
}
#endif
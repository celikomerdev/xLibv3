#if xLibv3
using UnityEngine;
using UnityEngine.SceneManagement;
using xLib.EventClass;

namespace xLib.ToolSceneUtility
{
	public class GetBuildIndexByScenePath : BaseMainM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("eventInt")]
		[SerializeField]private EventInt eventIndex = new EventInt();
		
		public void Call(string value)
		{
			int temp = SceneUtility.GetBuildIndexByScenePath(value);
			eventIndex.Invoke(temp);
		}
	}
}
#endif
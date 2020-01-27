#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolApplication
{
	public class ApplicationPlatform : BaseMainM
	{
		[SerializeField]private RuntimePlatform[] array = new RuntimePlatform[0];
		[SerializeField]private EventBool eventBool = new EventBool();
		
		private void Awake()
		{
			eventBool.Invoke(IsValid());
		}
		
		private bool IsValid()
		{
			for (int i = 0; i < array.Length; i++)
			{
				if(array[i] == Application.platform) return true;
			}
			return false;
		}
	}
}
#endif
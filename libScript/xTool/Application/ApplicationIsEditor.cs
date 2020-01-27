#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolApplication
{
	public class ApplicationIsEditor : BaseMainM
	{
		[SerializeField]private EventBool eventBool = new EventBool();
		
		private void Awake()
		{
			eventBool.Invoke(Application.isEditor);
		}
	}
}
#endif
#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolApplication
{
	public class ApplicationIsEditor : BaseM
	{
		public EventBool eventBool;
		
		private void Awake()
		{
			eventBool.Invoke(Application.isEditor);
		}
	}
}
#endif
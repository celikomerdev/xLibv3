#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolApplication
{
	public class ApplicationTarget : BaseM
	{
		public RuntimePlatform[] array;
		public EventBool eventBool;
		
		private void Awake()
		{
			eventBool.Invoke(IsValid());
		}
		
		private bool IsValid()
		{
			for (int i = 0; i < array.Length; i++)
			{
				if(array[i] == xApp.platformTarget) return true;
			}
			return false;
		}
	}
}
#endif
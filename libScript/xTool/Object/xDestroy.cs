#if xLibv3
using UnityEngine;

namespace xLib.ToolObject
{
	public class xDestroy : BaseMainM
	{
		[SerializeField]private Object target = null;
		
		public void Call()
		{
			Destroy(target);
		}
	}
}
#endif
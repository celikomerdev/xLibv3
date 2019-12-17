#if xLibv3
using UnityEngine;

namespace xLib.ToolPool
{
	public class PoolKey : BaseMainM
	{
		internal GameObject original;
		
		public void Pool()
		{
			MnPool.Pool(gameObject);
		}
	}
}
#endif
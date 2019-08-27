#if xLibv3
using UnityEngine;

namespace xLib.ToolPool
{
	public class PoolKey : BaseM
	{
		internal GameObject key;
		
		public void Pool()
		{
			MnPool.ins.Add(gameObject);
		}
	}
}
#endif
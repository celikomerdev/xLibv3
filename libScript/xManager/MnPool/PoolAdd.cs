#if xLibv3
using UnityEngine;

namespace xLib.ToolPool
{
	public class PoolAdd : BaseM
	{
		public GameObject[] value;
		public int count;
		
		public void Work()
		{
			for (int indexArray = 0; indexArray < value.Length; indexArray++)
			{
				for (int indexCount = 0; indexCount < count; indexCount++)
				{
					MnPool.ins.Get(value[indexArray]).GetComponent<PoolKey>().Pool();
				}
			}
		}
	}
}
#endif
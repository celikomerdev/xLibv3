#if xLibv3
using UnityEngine;

namespace xLib.ToolPool
{
	public class PoolAdd : BaseMainM
	{
		public GameObject[] value;
		public int count;
		
		public void Work()
		{
			for (int indexArray = 0; indexArray < value.Length; indexArray++)
			{
				for (int indexCount = 0; indexCount < count; indexCount++)
				{
					MnPool.Pool(MnPool.Spawn(value[indexArray],true));
				}
			}
		}
	}
}
#endif
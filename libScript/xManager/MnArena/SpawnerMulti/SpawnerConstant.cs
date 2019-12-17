#if xLibv2
using UnityEngine;

namespace xLib.xNew
{
	public class SpawnerConstant : SpawnerMulti
	{
		public Transform[] tranforms;
		public override Transform ParentTransform(int index,int max)
		{
			return tranforms[index];
		}
	}
}
#endif
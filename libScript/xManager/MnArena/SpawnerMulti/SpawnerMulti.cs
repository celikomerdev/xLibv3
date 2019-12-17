#if xLibv2
using UnityEngine;

namespace xLib.xNew
{
	public abstract class SpawnerMulti : SingletonM<SpawnerMulti>
	{
		public virtual Transform ParentTransform(int index,int max)
		{
			return transform;
		}
	}
}
#endif
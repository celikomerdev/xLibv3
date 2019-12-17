#if DiscardxLibv1
using UnityEngine;

namespace xLib.xNew
{
	public class SpawnerGrid : SpawnerMulti
	{
		public UIGrid grid;
		
		public override Transform ParentTransform(int index,int max)
		{
			for (int i = 0; i < max; i++)
			{
				GameObject temp = new GameObject();
				temp.transform.SetParent(grid.transform);
			}
			
			grid.Reposition();
			return grid.GetChild(index-1).transform;
		}
	}
}
#endif
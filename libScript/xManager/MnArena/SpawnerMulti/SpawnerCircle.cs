#if xLibv2
using UnityEngine;

namespace xLib.xNew
{
	public class SpawnerCircle : SpawnerMulti
	{
		public Transform spawnPivot;
		public Transform spawnParent;
		
		public override Transform ParentTransform(int index,int max)
		{
			float playerDegree = (360f/max)*index;
			spawnPivot.eulerAngles = new Vector3(0,playerDegree,0);
			if(CanDebug) Debug.LogFormat(this,this.name+":ParentTransform:{0}",playerDegree);
			
			return spawnParent;
		}
	}
}
#endif
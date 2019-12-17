#if xLibv3
using UnityEngine;

namespace xLib
{
	public class RemapChildren : BaseMainM
	{
		[SerializeField]private Transform trans = null;
		[SerializeField]private Transform target = null;
		[SerializeField]private int startAfter = 0;
		[SerializeField]private int endBefore = 0;
		
		private void OnTransformChildrenChanged()
		{
			enabled = true;
		}
		
		private void Update()
		{
			trans.RemapChildren(target:target,startAfter:startAfter,endBefore:endBefore);
			enabled = false;
		}
	}
}
#endif
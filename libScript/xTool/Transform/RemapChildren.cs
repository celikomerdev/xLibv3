#if xLibv3
using UnityEngine;

namespace xLib
{
	public class RemapChildren : BaseMainM
	{
		[SerializeField]private Transform trans;
		[SerializeField]private Transform target;
		[SerializeField]private int startAfter;
		[SerializeField]private int endBefore;
		
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
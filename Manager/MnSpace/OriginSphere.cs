#if xLibv2
using UnityEngine;

namespace xLib.ToolSpace
{
	public class OriginSphere : BaseTickM
	{
		public float sqrMagnitude = 1000000;
		
		protected override void Tick(float tickTime)
		{
			if(transform.position.sqrMagnitude<sqrMagnitude) return;
			MnSpace.ins.Translate(-transform.position);
		}
	}
}
#endif
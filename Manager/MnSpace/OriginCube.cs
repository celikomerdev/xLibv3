#if xLibv2
using UnityEngine;

namespace xLib.ToolSpace
{
	public class OriginCube : BaseTickM
	{
		public Transform trans;
		public float magnitude = 1000;
		private Vector3 translate = Vector3.zero;
		
		protected override void Tick(float tickTime)
		{
			if(trans.position.x > magnitude) translate.x = magnitude;
			else if(trans.position.x < -magnitude) translate.x = -magnitude;
			
			else if(trans.position.y > magnitude) translate.y = magnitude;
			else if(trans.position.y < -magnitude) translate.y = -magnitude;
			
			else if(trans.position.z > magnitude) translate.z = magnitude;
			else if(trans.position.z < -magnitude) translate.z = -magnitude;
			
			else return;
			
			MnSpace.ins.Translate(-translate);
			translate = Vector3.zero;
		}
		
		public float Magnitude
		{
			set
			{
				this.magnitude = value;
			}
		}
	}
}
#endif
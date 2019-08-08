#if xLibv3
using UnityEngine;

namespace xLib.xTool.ToolTransform
{
	public class Rotate : BaseTickNodeM
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 speed = Vector3.zero;
		[SerializeField]private Space space = Space.Self;
		
		protected override void Tick(float tickTime)
		{
			target.Rotate(tickTime*speed,space);
		}
		
		public Vector3 Speed
		{
			get
			{
				return speed;
			}
			set
			{
				speed = value;
			}
		}
	}
}
#endif
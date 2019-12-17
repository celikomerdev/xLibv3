#if xLibv3
using UnityEngine;
using xLib.Mathx;

namespace xLib.ToolFollow
{
	public class FollowTransform : BaseTickNodeM
	{
		[SerializeField]private Transform trans = null;
		[SerializeField]private Transform target = null;
		public Transform Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
				this.enabled = (value!=null);
			}
		}
		[SerializeField]private float lerp = 1;
		
		protected override void Tick(float tickTime)
		{
			if(lerp<100)
			{
				trans.SpacePositionSet(Vector3.Lerp(trans.SpacePositionGet(),target.SpacePositionGet(),lerp*tickTime));
				trans.eulerAngles = MathVector3.LerpAngle(trans.eulerAngles,target.eulerAngles,lerp*tickTime);
				trans.localScale = Vector3.Lerp(trans.localScale,target.localScale,lerp*tickTime);
			}
			else
			{
				Snap();
			}
		}
		
		public void Snap()
		{
			trans.position = target.position;
			trans.eulerAngles = target.eulerAngles;
			trans.localScale = target.localScale;
		}
	}
}
#endif
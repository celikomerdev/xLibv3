#if xLibv2
using UnityEngine;

namespace xLib.ToolSkidmark
{
	public class PointSkidmark : BaseM
	{
		public Transform trans;
		public float Intensity
		{
			get;
			set;
		}
		
		private int indexLast = -1;
		private void OnEnable()
		{
			indexLast = -1;
		}
		
		private void Update()
		{
			indexLast = MnSkidmark.ins.AddSkidMark(trans.SpacePositionGet(),trans.up,Intensity,indexLast);
		}
	}
}
#endif
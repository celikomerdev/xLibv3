#if xLibv2
using UnityEngine;
using UnityEngine.EventSystems;
using xLib.xInput;
using xLib.xNode.NodeObject;

namespace xLib.xTool.xInput
{
	public class TouchInputSmooth : BaseTickM
	{
		public NodeFloat axis;
		public float target;
		[UnityEngine.Serialization.FormerlySerializedAs("valueLerp")]
		public float lerp = 4;
		
		#region Behavior
		protected override void Tick(float tickTime)
		{
			axis.Value = Mathf.MoveTowards(axis.Value,target,lerp*tickTime);
			if(axis.Value == Target) IsActive = false;
		}
		#endregion
		
		#region Public
		public float Target
		{
			get
			{
				return target;
			}
			set
			{
				if(target == value) return;
				target = value;
				IsActive = true;
			}
		}
		
		public void TargetAdd(float value)
		{
			Target += value;
		}
		#endregion
	}
}
#endif
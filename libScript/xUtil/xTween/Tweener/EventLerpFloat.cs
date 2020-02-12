#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xUtil
{
	public class EventLerpFloat : BaseTickNodeM
	{
		[SerializeField]private float lerp = 4;
		
		
		#region Public
		[SerializeField]private float current = 0;
		[SerializeField]private float target = 0;
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
		
		
		#region Behavior
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		protected override void Tick(float tickTime)
		{
			current = Mathf.MoveTowards(current,target,lerp*tickTime);
			eventFloat.Value = current;
			if(current == target) IsActive = false;
		}
		#endregion
	}
}
#endif
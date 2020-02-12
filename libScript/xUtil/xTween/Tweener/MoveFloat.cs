#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolLerp
{
	public class MoveFloat : BaseTickNodeM
	{
		[SerializeField]private float current = 0;
		[SerializeField]private float target = 0;
		[SerializeField]private float speed = 4;
		
		
		#region Behavior
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		protected override void Tick(float tickTime)
		{
			if(speed<100) current = Mathf.MoveTowards(current,target,tickTime*speed);
			else current = target;
			
			eventFloat.Invoke(current);
			if(current == target) IsActive = false;
		}
		#endregion
		
		
		#region Main
		public float Current
		{
			get
			{
				return current;
			}
			set
			{
				if(this.current == value) return;
				this.current = value;
				IsActive = true;
			}
		}
		
		public float Target
		{
			get
			{
				return target;
			}
			set
			{
				if(this.target == value) return;
				this.target = value;
				IsActive = true;
			}
		}
		
		public float Speed
		{
			get
			{
				return speed;
			}
			set
			{
				this.speed = value;
			}
		}
		#endregion
		
		
		#region Add
		public void AddCurrent(float value)
		{
			Current += value;
		}
		
		public void AddTarget(float value)
		{
			Target += value;
		}
		#endregion
	}
}
#endif
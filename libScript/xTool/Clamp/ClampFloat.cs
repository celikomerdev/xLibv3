#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolClamp
{
	public class ClampFloat : BaseMainM
	{
		[SerializeField]private float value = 0;
		[SerializeField]private float min = float.MinValue;
		[SerializeField]private float max = float.MaxValue;
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		public float Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if(this.value == value) return;
				this.value = Mathf.Clamp(value,min,max);
				eventFloat.Invoke(this.value);
			}
		}
		
		public float ValueAdd
		{
			get
			{
				return (this.value + value);
			}
			set
			{
				Value += value;
			}
		}
		
		public float Min
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}
		
		public float Max
		{
			get
			{
				return this.max;
			}
			set
			{
				this.max = value;
			}
		}
	}
}
#endif
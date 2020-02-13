#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolClamp
{
	public class ClampInt : BaseMainM
	{
		[SerializeField]private int value = 0;
		[SerializeField]private int min = int.MinValue;
		[SerializeField]private int max = int.MaxValue;
		[SerializeField]private EventInt eventInt = new EventInt();
		
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if(this.value == value) return;
				this.value = Mathf.Clamp(value,min,max);
				eventInt.Invoke(this.value);
			}
		}
		
		public int ValueAdd
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
		
		public int Min
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
		
		public int Max
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
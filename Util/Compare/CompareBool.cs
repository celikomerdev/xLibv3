#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareBool : BaseWorkM
	{
		[SerializeField]private bool value;
		public bool Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}
		
		public EventUnity onEqual;
		public void Compare(bool value)
		{
			if(!CanWork) return;
			bool result = (this.value == value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareBool:{0}",result);
			
			if(!result) return;
			onEqual.Invoke();
		}
	}
}
#endif